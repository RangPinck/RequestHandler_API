using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.DTO;
using RequestHandler.Interfaces;

namespace RequestHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _repositoryD;
        private readonly IUserRepository _repositoryU;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentRepository repositoryD,
            IUserRepository repositoryU, IMapper mapper)
        {
            _repositoryD = repositoryD;
            _repositoryU = repositoryU;
            _mapper = mapper;
        }

        [HttpGet("GetDocuments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DocumentDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDocuments(Guid userId)
        {
            var documents = _mapper.Map<List<DocumentDto>>(
                await _repositoryD.GetDocuments(userId));
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(documents);
        }

        [HttpPost("UploadDocument")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadDocument(Guid logUserId, Guid? appointmentId, IFormFile file)
        {
            if (!await _repositoryU.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            string title = Path.GetFileName(file.FileName) + extension;

            await _repositoryD.UploadDocument(appointmentId, title);
            if (!await _repositoryD.UploadFile(file, title))
            {
                ModelState.AddModelError("", "Somthing went wrong uploating document.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully uploaded.");
        }

        [HttpGet("DownloadDocument")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<FileContentResult> DownloadDocument(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception($"No correct request: title is null or empty.");

            if (!await _repositoryD.DocumentExists(title))
                throw new FileNotFoundException("File not found.");

            var file = await _repositoryD.DownloadFile(title);

            if (!ModelState.IsValid)
                throw new FileNotFoundException("File not found.");

            return file;
        }
    }
}
