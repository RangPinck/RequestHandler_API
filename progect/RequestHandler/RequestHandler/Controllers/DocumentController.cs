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

        [HttpPost("UploadDocument")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadDocument(Guid logUserId, Guid? appointmentId, IFormFile file)
        {
            if (!await _repositoryU.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            string title = Path.GetFileName(file.FileName) + extension;

            if (!await _repositoryD.UploadDocument(appointmentId, title))
            {
                ModelState.AddModelError("", "Somthing went wrong uploating document.");
                return StatusCode(500, ModelState);
            }

            if (!await _repositoryD.UploadFile(file, title))
            {
                ModelState.AddModelError("", "Somthing went wrong uploating document.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully uploaded.");
        }

        [HttpDelete("DeleteDocument")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDocument(Guid logUserId, Guid doucumentId)
        {
            if (!await _repositoryU.ValidateAdmin(logUserId))
                return BadRequest($"No correct request: user with \"{logUserId}\" id not validation.");

            if (!await _repositoryD.DocumentExists(doucumentId))
                return NotFound($"Document with id \"{doucumentId}\" not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repositoryD.DeleteFile(doucumentId))
            {
                ModelState.AddModelError("", "Somthing went wrong deleting doucument.");
                return StatusCode(500, ModelState);
            }

            if (!await _repositoryD.DeleteDocument(doucumentId))
            {
                ModelState.AddModelError("", "Somthing went wrong deleting doucument.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successefully deleted.");
        }
    }
}
