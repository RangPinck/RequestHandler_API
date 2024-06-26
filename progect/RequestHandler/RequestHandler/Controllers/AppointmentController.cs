using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using System.Data;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _repositoryA;
        private readonly IUserRepository _repositoryU;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository repositoryA,IUserRepository repositoryU, IMapper mapper)
        {
            _repositoryA = repositoryA;
            _repositoryU = repositoryU;
            _mapper = mapper;
        }

        //проверить работу
        [HttpGet("GetAppointments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentGetDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppointments(Guid logUserId, int roleId = 4)
        {
            if (!await _repositoryU.ValidateAdmin(logUserId)
                && !await _repositoryU.ValidateApproval(logUserId)
                && !await _repositoryU.ValidateMaster(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (roleId <= 0 || roleId >= 5)
                return BadRequest($"No correct request: {roleId} can't be more 4 and can't be less 1.");

            var approvs = _mapper.Map<List<AppointmentGetDto>>(
                await _repositoryA.GetAppointments(roleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(approvs);
        }

        [HttpPost("CreateAppointment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAppointment(Guid userId, string Problem, string? DiscriptionProblem, string Place)
        {
            if (string.IsNullOrEmpty(Problem))
                return BadRequest($"No correct request: Problem is null or empty.");

            if (string.IsNullOrEmpty(Place))
                return BadRequest($"No correct request: Place is null or empty.");

            if (!await _repositoryU.UserExists(userId))
                return NotFound($"User with id \"{userId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var create = await _repositoryA.CreateAppointment(
                userId: userId,
                Problem: Problem,
                DiscriptionProblem: DiscriptionProblem,
                Place: Place
                );

            if (!create)
            {
                ModelState.AddModelError("", $"Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully created.");
        }

    }
}
