using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using RequestHandler.DTO;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllUsers(Guid logUserId, int? roleId = null)
        {
            if (roleId <= 0 || roleId >= 5)
                return BadRequest($"No correct request: {roleId} can't be more 4 and can't be less 1.");

            if (!await _repository.ValidateAdmin(logUserId)
                && !await _repository.ValidateApproval(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            var users =
                _mapper.Map<List<UserDto>>(
                await _repository.GetAllUsers(roleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }


        [HttpGet("Authorithation")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authorithation(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                return BadRequest($"No correct request: login is null or empty.");

            if (string.IsNullOrEmpty(password))
                return BadRequest($"No correct request: password is null or empty.");

            if (!await _repository.UserExists(login))
                return NotFound($"User with login {login} not found.");

            var user =
                _mapper.Map<UserDto>(
                await _repository.Authorithation(login, password));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest($"No correct request: password is not correct.");

            return Ok(user);
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(Guid logUserId, string login, string password, string surname, string name, int role)
        {
            if (!await _repository.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (string.IsNullOrEmpty(login))
                return BadRequest($"No correct request: login is null or empty.");

            if (string.IsNullOrEmpty(password))
                return BadRequest($"No correct request: password is null or empty.");

            if (string.IsNullOrEmpty(surname))
                return BadRequest($"No correct request: surname is null or empty.");

            if (string.IsNullOrEmpty(name))
                return BadRequest($"No correct request: name is null or empty.");

            if (role <= 0 || role >= 5)
                return BadRequest($"No correct request: {role} can't be more 4 and can't be less 1.");

            if (await _repository.UserExists(login))
            {
                ModelState.AddModelError("", $"User with login {login} already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var create = await _repository.CreateUser(login, password, surname, name, role);

            if (!create)
            {
                ModelState.AddModelError("", $"Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully created.");
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(Guid logUserId, Guid id, string? login = null, string? password = null, string? surname = null, string? name = null, int? role = null)
        {
            if (!await _repository.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (!await _repository.UserExists(id))
                return NotFound($"User with id \"{id}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.UpdateUser(
                id: id,
                login: login,
                password: password,
                surname: surname,
                name: name,
                role: role))
            {
                ModelState.AddModelError("","Somthing went wrong updateing user.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully updated.");
        }
   
        [HttpDelete("DeleteUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(Guid logUserId, Guid id)
        {
            if (!await _repository.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (!await _repository.UserExists(id))
                return NotFound($"User with id \"{id}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _repository.DeleteUser(id))
            {
                ModelState.AddModelError("", "Somthing went wrong deleting user.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully deleted.");
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var uploadPath = $"{Directory.GetCurrentDirectory()}\\LocalFiles";

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fullPath = $"{uploadPath}\\{file.FileName}";

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok($"Successefully upload.{file.FileName}");
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileTitle)
        {
            //var uploadPath = $"{Directory.GetCurrentDirectory()}\\LocalFiles\\{fileTitle}";
            //var provider = new FileExtensionContentTypeProvider();

            //if (!provider.TryGetContentType(uploadPath, out var contentType))
            //{
            //    contentType = "application/octet-stream";
            //}

            //var bytes = await System.IO.File.ReadAllBytesAsync(uploadPath);

            //return File(bytes,contentType,Path.GetFileName(uploadPath));
        }
    }
}
