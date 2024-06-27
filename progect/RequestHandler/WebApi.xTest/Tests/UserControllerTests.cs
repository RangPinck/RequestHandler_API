using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.Controllers;
using RequestHandler.Interfaces;
using RequestHandler.Mapping;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.xTest.FakeDate;

namespace WebApi.xTest.Tests
{
    public class UserControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userRepository = new FakeUserRepository();

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

            _userController = new UserController(_userRepository, _mapper);
        }

        [Fact]
        public async void Authorization_ReturnOk()
        {
            string login = "Ivan_Kuznetsov";
            string password = "12345";
            var item = await _userController.Authorithation(login, password);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void CreateUser_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            string login = "Chernov_Sergey";
            string password = "12345";
            string surname = "Чернов";
            string name = "Сергей";
            int role = 1;
            var item = await _userController.CreateUser(
                logUserId: logUserId,
                login: login,
                password: password,
                name: name,
                role: role,
                surname: surname);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void GetAllUsers_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            int? role = null;
            var item = await _userController.GetAllUsers(logUserId:logUserId, roleId: role);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void UpdateUser_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            Guid id = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8");
            string ? login = "Sergey_Chernov";
            var item = await _userController.UpdateUser(logUserId: logUserId, id: id, login: login);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void DeleteUser_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            Guid id = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8");
            var item = await _userController.DeleteUser(logUserId: logUserId, id: id);
            Assert.IsType<OkObjectResult>(item);
        }
    }
}
