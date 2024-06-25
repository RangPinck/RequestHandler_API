using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Models;
using RequestHandler.Repositories;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly RolesRepository _repository;
        private readonly IMapper _mapper;

        public RolesController(RolesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //проверить ещё раз (доступность и маппинг)
        [HttpGet("GetRoles")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RolesDto>))]
        public async Task<IActionResult> GetRoles()
        {
            var roles =
                _mapper.Map<List<RolesDto>>(
                await _repository.GetRoles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roles);
        }
    }
}
