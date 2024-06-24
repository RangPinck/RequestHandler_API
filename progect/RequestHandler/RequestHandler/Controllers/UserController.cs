using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        //сделать проверку на пустой результат
        //сделать обработку невалидного состояния пользователя
        //сделать преобразование к нормальному виду ответа
        public async Task<IActionResult> GetAllUsers(Guid logUserId,int? roleId = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.ValidateAdmin(logUserId)
                && !await _repository.ValidateApproval(logUserId))
                return BadRequest(ModelState);

            var users = _repository.GetAllUsers(roleId);

            return Ok(users);
        }
    }
}
