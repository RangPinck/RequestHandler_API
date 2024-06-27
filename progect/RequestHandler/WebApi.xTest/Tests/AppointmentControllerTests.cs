using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RequestHandler.Controllers;
using RequestHandler.Interfaces;
using RequestHandler.Mapping;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.xTest.FakeDate;

namespace WebApi.xTest.Tests
{
    public class AppointmentControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _repositoryA;
        private readonly IUserRepository _repositoryU;
        private readonly AppointmentController _controller;

        public AppointmentControllerTests()
        {
            _repositoryA = new FakeAppointmentRepository();
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

            _controller = new AppointmentController(_repositoryA,_repositoryU, _mapper);
        }

        [Fact]
        public async void GetAppointments_ReturnOk()
        {
            Guid logUserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B");
            int roleId = 4;
            var item = await _controller.GetAppointments(logUserId,roleId);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void CreateAppointment_ReturnOk()
        {
            Guid userId = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8");
            string Problem = "Сломался чайник"; 
            string? DiscriptionProblem = null; 
            string Place = "5-тый корпус, 11 этаж, 107 кабинет";

            var item = await _controller.CreateAppointment(userId, Problem, DiscriptionProblem, Place);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void UpdateAppointment_ReturnOk()
        {
            Guid appointmentId = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52");
            string Problem = "Сломался чайник";
            string? DiscriptionProblem = null;
            string Place = "5-тый корпус, 11 этаж, 107 кабинет";

            var item = await _controller.UpdateAppointment(appointmentId, Problem, DiscriptionProblem, Place);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void UpdateAppointmentApprove_ReturnOk()
        {
            Guid userId = Guid.Parse("28ABB33E-D9AF-449E-AD72-C3510935612B");
            Guid appointmentId = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52");

            var item = await _controller.UpdateAppointmentApprove(userId, appointmentId);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void UpdateAppointmentFix_ReturnOk()
        {
            Guid userId = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8");
            Guid appointmentId = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52");

            var item = await _controller.UpdateAppointmentFix(userId, appointmentId);
            Assert.IsType<OkObjectResult>(item);
        }

        [Fact]
        public async void DeleteAppointment_ReturnOk()
        {
            Guid appointmentId = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52");

            var item = await _controller.DeleteAppointment(appointmentId);
            Assert.IsType<OkObjectResult>(item);
        }

    }
}
