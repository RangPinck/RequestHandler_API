using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllUsers(Guid logUserId, int? roleId = null)
        {
            if (roleId <= 0 || roleId >= 5)
                return BadRequest($"No correct request: {roleId} can't be more 4 and can't be less 1.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.ValidateAdmin(logUserId)
                && !await _repository.ValidateApproval(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            var users =
                _mapper.Map<List<UserDto>>(
                await _repository.GetAllUsers(roleId));

            return Ok(users);
        }


        [HttpGet("Authorithation")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authorithation(string login, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(login))
                return BadRequest($"No correct request: login is null or empty");

            if (string.IsNullOrEmpty(password))
                return BadRequest($"No correct request: password is null or empty");

            if (!await _repository.UserExists(login))
                return NotFound($"User with login {login} not found.");

            var user =
                _mapper.Map<UserDto>(
                await _repository.Authorithation(login, password));

            if (user == null)
                return BadRequest($"No correct request: password is not correct.");

            return Ok(user);
        }
    }
}
