using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;
using RequestHandler.Models;

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        //сделать проверку на пустой результат
        [ProducesResponseType(400)]
        //сделать преобразование к нормальному виду ответа
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
    }
}
