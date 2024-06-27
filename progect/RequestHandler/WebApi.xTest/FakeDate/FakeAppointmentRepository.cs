using Microsoft.EntityFrameworkCore;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.xTest.FakeDate
{
    public class FakeAppointmentRepository : IAppointmentRepository
    {
        private readonly List<UserAppointment> _fakeUserAppointments;
        private readonly List<Appointment> _fakeAppointments;
        private readonly List<User> _fakeUsers;

        public FakeAppointmentRepository()
        {
            _fakeUsers = new List<User>()
            {
                new User()
                {
                    UserId = Guid.Parse("38C13A21-ABE4-40C3-9877-33126F386E7B"),
                    Login = "Ivan_Kuznetsov",
                    //12345
                    Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5",
                    Surname = "Кузнецов",
                    Name = "Иван",
                    Role = 1
                },
                new User()
                {
                    UserId = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8"),
                    Login = "Maria_Ivanova",
                    //67891
                    Password = "44011a51f70a067c690dfb9959cdab5d7c37a28044f7604f047fd9dafb45cd02",
                    Surname = "Иванова",
                    Name = "Мария",
                    Role = 2
                },
                new User()
                {
                    UserId = Guid.Parse("28ABB33E-D9AF-449E-AD72-C3510935612B"),
                    Login = "Peter_Sokolov",
                    //01112
                    Password = "3d60c92d2c4610ac1245238497b7902e66eef0ec669d2907ddeaa7e40254df41",
                    Surname = "Соколов",
                    Name = "Пётр",
                    Role = 3
                },
                new User()
                {
                    UserId = Guid.Parse("23CE0334-8083-412F-AEE6-1F3FF7B1DD04"),
                    Login = "Olga_Vasilieva",
                    //13141
                    Password = "986158efae5d9b5106d797a9f7bb4a990c1ddcbb9460de3259241b798d37d0b9",
                    Surname = "Васильева",
                    Name = "Ольга",
                    Role = 4
                },
                new User()
                {
                    UserId = Guid.Parse("1DCA5D23-E9CD-48F2-A343-F0F58644AD6C"),
                    Login = "Jacob_Sidorov",
                    //51617
                    Password = "54037ac535a79aab88e96fa2321265456b637e9c679f5f8ed1d7962bd805cad1",
                    Surname = "Сидоров",
                    Name = "Яков",
                    Role = 4
                }
            };
            _fakeAppointments = new List<Appointment>()
            {
                new Appointment()
                {
                    AppointmentId = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52"),
                    Problem = "Установка ПО",
                    DiscriptionProblem = "Пожалуйста, установите такие прогрммы как Visual Studio, Git Bash и 1C.",
                    Place = "Отдел управления цехами",
                    Status = 2,
                    DateCreate = new DateTime(2024,6,4,12,0,0),
                    DateApprove = new DateTime(2024,6,5,12,0,0),
                    DateFix = null
                },
                new Appointment()
                {
                    AppointmentId = Guid.Parse("FE1AC267-8732-484B-BE2D-CD7566A4E43B"),
                    Problem = "Протёк куллер",
                    DiscriptionProblem = null,
                    Place = "Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет",
                    Status = 1,
                    DateCreate = new DateTime(2024,6,27,12,0,0),
                    DateApprove = null,
                    DateFix = null
                },
                new Appointment()
                {
                    AppointmentId = Guid.Parse("C2B5F9CB-F033-4004-8862-84C34AB13501"),
                    Problem = "установка ПО",
                    DiscriptionProblem = "Пожалуйста, установите 1C.",
                    Place = "Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет",
                    Status = 3,
                    DateCreate = new DateTime(2024,6,3,12,0,0),
                    DateApprove = new DateTime(2024,6,4,12,0,0),
                    DateFix = new DateTime(2024,6,5,12,0,0)
                },
                new Appointment()
                {
                    AppointmentId = Guid.Parse("31E53662-E7AB-42CF-BD7D-48B2717152AA"),
                    Problem = "Компьютер не видит принтер",
                    DiscriptionProblem = null,
                    Place = "Бухгалтерия, 4-тое здание, 7 этаж, 1 кабинет",
                    Status = 3,
                    DateCreate = new DateTime(2024,6,1,9,0,0),
                    DateApprove = new DateTime(2024,6,2,9,0,0),
                    DateFix = new DateTime(2024,6,3,12,0,0)
                },
                new Appointment()
                {
                    AppointmentId = Guid.Parse("22F8DD73-D48C-441D-BFE0-2D488947E111"),
                    Problem = "Сломался телефон",
                    DiscriptionProblem = "При звонке не происходит переадрисация.",
                    Place = "Бухгалтерия, 4-тое здание, 11 этаж, 10 кабинет",
                    Status = 2,
                    DateCreate = new DateTime(2024,6,27,12,0,0),
                    DateApprove = new DateTime(2024,6,27,14,0,0),
                    DateFix = null
                }
            };
            _fakeUserAppointments = new List<UserAppointment>()
            {
                new UserAppointment()
                {
                    Appointment = Guid.Parse("22F8DD73-D48C-441D-BFE0-2D488947E111"),
                    User = Guid.Parse("1DCA5D23-E9CD-48F2-A343-F0F58644AD6C")
                },
                new UserAppointment()
                {
                    Appointment = Guid.Parse("31E53662-E7AB-42CF-BD7D-48B2717152AA"),
                    User = Guid.Parse("86716A63-8093-4706-A975-0046FBEC60F8")
                },
                new UserAppointment()
                {
                    Appointment = Guid.Parse("C2B5F9CB-F033-4004-8862-84C34AB13501"),
                    User = Guid.Parse("23CE0334-8083-412F-AEE6-1F3FF7B1DD04")
                },
                new UserAppointment()
                {
                    Appointment = Guid.Parse("FE1AC267-8732-484B-BE2D-CD7566A4E43B"),
                    User = Guid.Parse("28ABB33E-D9AF-449E-AD72-C3510935612B")
                },
                new UserAppointment()
                {
                    Appointment = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52"),
                    User = Guid.Parse("1DCA5D23-E9CD-48F2-A343-F0F58644AD6C")
                },
            };
        }

        public async Task<bool> AppointmentExists(Guid appointmentId)
        {
            return _fakeAppointments.Any(a => a.AppointmentId == appointmentId);
        }

        public async Task<bool> CreateAppointment(Guid userId, string Problem, string? DiscriptionProblem, string Place)
        {
            Guid appId = Guid.NewGuid();

            Appointment appointment = new Appointment()
            {
                AppointmentId = appId,
                Problem = Problem,
                DiscriptionProblem = DiscriptionProblem,
                Place = Place,
                DateCreate = DateTime.UtcNow
            };

            _fakeAppointments.Add(appointment);

            _fakeUserAppointments.Add(new UserAppointment()
            {
                Appointment = appId,
                User = userId
            });

            return await Save();
        }

        public async Task<bool> DeleteAppointment(Guid appointmentId)
        {
            var app = _fakeAppointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            _fakeAppointments.Remove(app);
            return await Save();
        }

        public async Task<ICollection<UserAppointment>> GetAppointments(int roleId = 4)
        {
            if (roleId > 4 || roleId < 1)
            {
                return _fakeUserAppointments;
            }

            List<User> us = _fakeUsers.Where(u => u.Role == roleId).ToList();    
            List<Guid> usId = new List<Guid>();

            foreach (var item in us)
            {
                usId.Add(item.UserId);
            }

            return _fakeUserAppointments.Where(ua => usId.Contains(ua.User))
                .ToList();
        }

        public async Task<bool> Save()
        {
            return true;
        }

        public async Task<bool> UpdateAppointment(Guid appointmentId, string? Problem, string? DiscriptionProblem, string? Place)
        {
            var app = _fakeAppointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            app.Problem = string.IsNullOrEmpty(Problem) ? app.Problem : Problem;
            app.DiscriptionProblem = string.IsNullOrEmpty(DiscriptionProblem) ? app.DiscriptionProblem : DiscriptionProblem;
            app.Place = string.IsNullOrEmpty(Place) ? app.Place : Place;

            return await Save();
        }

        public async Task<bool> UpdateAppointmentApprove(Guid userId, Guid appointmentId)
        {
            var app = _fakeAppointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            app.DateApprove = DateTime.UtcNow;
            app.Status = 2;

            _fakeUserAppointments.Add(new UserAppointment()
            {
                Appointment = appointmentId,
                User = userId
            });

            return await Save();
        }

        public async Task<bool> UpdateAppointmentFix(Guid userId, Guid appointmentId)
        {
            var app = _fakeAppointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            app.DateApprove = DateTime.UtcNow;
            app.Status = 3;

            _fakeUserAppointments.Add(new UserAppointment()
            {
                Appointment = appointmentId,
                User = userId
            });

            return await Save();
        }
    }
}
