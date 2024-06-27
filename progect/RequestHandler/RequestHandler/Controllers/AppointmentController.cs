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

        public AppointmentController(IAppointmentRepository repositoryA, IUserRepository repositoryU, IMapper mapper)
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


        [HttpPut("UpdateAppointment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAppointment(Guid appointmentId, string? Problem, string? DiscriptionProblem, string? Place)
        {
            if (!await _repositoryA.AppointmentExists(appointmentId))
                return NotFound($"Appointment with id \"{appointmentId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repositoryA.UpdateAppointment(
                appointmentId: appointmentId,
                Problem: Problem,
                DiscriptionProblem: DiscriptionProblem,
                Place: Place
                ))
            {
                ModelState.AddModelError("", "Somthing went wrong updateing appointment.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully updated.");
        }

        //проверить работу
        [HttpPut("UpdateAppointmentApprove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAppointmentApprove(Guid userId, Guid appointmentId)
        {
            if (!await _repositoryU.ValidateApproval(userId))
                return BadRequest($"No correct request: user with \"{userId}\" id not validation.");

            if (!await _repositoryA.AppointmentExists(appointmentId))
                return NotFound($"Appointment with id \"{appointmentId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repositoryA.UpdateAppointmentApprove(
                   userId: userId,
                   appointmentId: appointmentId
                ))
            {
                ModelState.AddModelError("", "Somthing went wrong approved appointment.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully approved.");
        }

        //проверить работу
        [HttpPut("UpdateAppointmentFix")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAppointmentFix(Guid userId, Guid appointmentId)
        {
            if (!await _repositoryU.ValidateMaster(userId))
                return BadRequest($"No correct request: user with \"{userId}\" id not validation.");

            if (!await _repositoryA.AppointmentExists(appointmentId))
                return NotFound($"Appointment with id \"{appointmentId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repositoryA.UpdateAppointmentFix(
                   userId: userId,
                   appointmentId: appointmentId
                ))
            {
                ModelState.AddModelError("", "Somthing went wrong approved fixing.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully fixed.");
        }

        [HttpDelete("DeleteAppointment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
        {
            if (!await _repositoryA.AppointmentExists(appointmentId))
                return NotFound($"Appointment with id \"{appointmentId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repositoryA.DeleteAppointment(appointmentId))
            {
                ModelState.AddModelError("", "Somthing went wrong deleting appointment.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully deleted.");
        }
    }
}
