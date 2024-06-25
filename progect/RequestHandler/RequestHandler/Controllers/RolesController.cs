using Microsoft.AspNetCore.Mvc;
using RequestHandler.Models;
using RequestHandler.Repositories;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly RolesRepository _repository;

        public RolesController(RolesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetRoles")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public async Task<IActionResult> GetRoles()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roles = await _repository.GetRoles();

            return Ok(roles);
        }
    }
}
