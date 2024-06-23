using Microsoft.AspNetCore.Mvc;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using System.Text.Json.Serialization;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository user)
        {
            _userRepository = user;
        }

        [HttpGet, FormatFilter]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public JsonResult GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            var json = JsonConverter.S
            return Json("", JsonRe);
        }
    }
}
