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

    }
}
