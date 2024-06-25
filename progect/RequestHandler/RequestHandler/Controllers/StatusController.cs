using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _repository;
        private readonly IMapper _mapper;

        public StatusController(IStatusRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetStatuses")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StatusDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatuses()
        {
            var status =
                _mapper.Map<List<StatusDto>>(
                await _repository.GetStatuses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(status);
        }
    }
}
