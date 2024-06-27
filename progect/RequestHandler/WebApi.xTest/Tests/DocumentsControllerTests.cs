using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RequestHandler.Controllers;
using RequestHandler.Interfaces;
using RequestHandler.Mapping;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi.xTest.FakeDate;

namespace WebApi.xTest.Tests
{
    public class DocumentsControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _repositoryD;
        private readonly IUserRepository _repositoryU;
        private readonly DocumentController _controller;

        public DocumentsControllerTests()
        {
            _repositoryD = new FakeDocumentRepository();
            _repositoryU = new FakeUserRepository();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mp =>
                {
                    mp.AddProfile(new MappingProfile());
                }
                );
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _controller = new DocumentController(_repositoryD,_repositoryU, _mapper);
        }

        [Fact]
        public async void GetDocuments_ReturnOk()
        {
            Guid userId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            var item = await _controller.GetDocuments(userId);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void DownloadDocument_FileContentResult()
        {
            string title = "fi.txt";

            var item = await _controller.DownloadDocument(title);
            Assert.IsType<FileContentResult>(item);
        }

        [Fact]
        public async void UploadDocument_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            Guid? appointmentId = null;

            // Создание объекта MemoryStream для хранения данных файла
            var filepath = Path.Combine("D:\\Progect\\forPractice\\progect\\RequestHandler\\WebApi.xTest\\LocalFiles\\", "fi.txt");
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filepath);
            IFormFile file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "Data", "fi.txt");

            var item = await _controller.UploadDocument(logUserId, appointmentId, file);
            Assert.IsType<OkObjectResult>(item);
        }

        //DeleteDocument(Guid logUserId, Guid doucumentId)
        [Fact]
        public async void DeleteDocument_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            Guid doucumentId = Guid.Parse("0F739B90-2185-4524-958E-77B62C5F003A");

            var item = await _controller.DeleteDocument(logUserId, doucumentId);
            Assert.IsType<OkObjectResult>(item);
        }

    }
}
