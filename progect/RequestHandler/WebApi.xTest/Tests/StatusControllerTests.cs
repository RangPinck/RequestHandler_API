using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.Controllers;
using RequestHandler.Interfaces;
using RequestHandler.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.xTest.FakeDate;

namespace WebApi.xTest.Tests
{
    public class StatusControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IStatusRepository _repository;
        private readonly StatusController _controller;

        public StatusControllerTests()
        {
            _repository = new FakeStatusRepository();

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

            _controller = new StatusController(_repository, _mapper);
        }

        [Fact]
        public async void GetStatuses_ReturnOk()
        {
            var item = await _controller.GetStatuses();
            Assert.IsType<OkObjectResult>(item);
        }
    }
}
