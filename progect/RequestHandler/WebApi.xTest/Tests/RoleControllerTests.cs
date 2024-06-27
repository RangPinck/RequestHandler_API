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
    public class RoleControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IRolesRepository _repository;
        private readonly RolesController _controller;

        public RoleControllerTests()
        {
            _repository = new FakeRoleRepository();

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

            _controller = new RolesController(_repository, _mapper);
        }

        [Fact]
        public async void GetRoles_ReturnOk()
        {
            var item = await _controller.GetRoles();
            Assert.IsType<OkObjectResult>(item);
        }
    }
}
