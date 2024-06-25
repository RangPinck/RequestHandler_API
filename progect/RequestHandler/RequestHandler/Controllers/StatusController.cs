using Microsoft.AspNetCore.Mvc;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _repository;

        public StatusController(IStatusRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet("GetStatuses")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Status>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatuses()
        {
            var status = await _repository.GetStatuses();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(status);
        }
    }
}
