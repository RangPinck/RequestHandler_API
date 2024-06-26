using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;
using RequestHandler.Models;

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
        public async Task<IActionResult> GetAppointments(Guid logUserId, int roleId)
        {
            if (!await _repositoryU.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (!await _repositoryU.ValidateApproval(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (!await _repositoryU.ValidateMaster(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");
            
            var approvs = _mapper.Map<List<AppointmentGetDto>>(
                await _repositoryA.GetAppointments(roleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(approvs);
        }

    }
}
