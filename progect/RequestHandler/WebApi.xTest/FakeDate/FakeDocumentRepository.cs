using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
    public class FakeDocumentRepository : IDocumentRepository
    {
        private readonly List<Document> _fakeDocuments;
        private readonly List<User> _fakeUsers;
        private readonly List<UserAppointment> _fakeUserAppointments;

        public FakeDocumentRepository()
        {
            _fakeDocuments = new List<Document>()
            {
                new Document()
                {
                    DocumentId = Guid.Parse("BB7587F4-BD7E-4031-9E26-10C64B48E41C"),
                    Title = "Накладная.txt",
                    Appointment = Guid.Parse("D0451E80-5123-41F1-9BDE-E9D51E646C52"),
                },
                new Document()
                {
                    DocumentId = Guid.Parse("DE331327-0BDC-44A8-87F5-2E802E77BA7B"),
                    Title = "Схема.txt",
                    Appointment = Guid.Parse("31E53662-E7AB-42CF-BD7D-48B2717152AA"),
                },
                new Document()
                {
                    DocumentId = Guid.Parse("0F739B90-2185-4524-958E-77B62C5F003A"),
                    Title = "fi.txt",
                    Appointment = null,
                },
                new Document()
                {
                    DocumentId = Guid.Parse("2D2B5AB7-F12C-41CD-BFD6-D9111A5BC502"),
                    Title = "Фото.txt",
                    Appointment = Guid.Parse("FE1AC267-8732-484B-BE2D-CD7566A4E43B"),
                },
                new Document()
                {
                    DocumentId = Guid.Parse("9FFF078F-0AA2-4E0D-9283-F522C4115DE5"),
                    Title = "Список_ip.txt",
                    Appointment = Guid.Parse("22F8DD73-D48C-441D-BFE0-2D488947E111"),
                },
            };
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

        public async Task<bool> DeleteDocument(Guid doucumentId)
        {
            var doc = _fakeDocuments.First(
                 d => d.DocumentId == doucumentId);

            _fakeDocuments.Remove(doc);
            return await Save();
        }

        public async Task<bool> DeleteFile(Guid doucumentId)
        {
            Document file =  _fakeDocuments.First(d => d.DocumentId == doucumentId);
            string title = file.Title;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title);
            try
            {
                FileInfo fileInf = new FileInfo(filepath);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DocumentExists(Guid doucumentId)
        {
            return _fakeDocuments.Any(d => d.DocumentId == doucumentId);
        }

        public async Task<bool> DocumentExists(string title)
        {
            return _fakeDocuments.Any(d => d.Title == title);
        }

        public async Task<bool> DocumentExistsOfAppointment(Guid appointmentId)
        {
            return _fakeDocuments.Any(d => d.Appointment == appointmentId);
        }

        public async Task<FileContentResult> DownloadFile(string title)
        {
            var filepath = Path.Combine("D:\\Progect\\forPractice\\progect\\RequestHandler\\WebApi.xTest\\LocalFiles\\", title);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return new FileContentResult(bytes, contenttype)
            {
                FileDownloadName = title
            };
        }

        public async Task<ICollection<Document>> GetDocuments(Guid userId)
        {
            User user = _fakeUsers.First(u => u.UserId == userId);

            switch (user.Role)
            {
                case < 4:
                    return _fakeDocuments.ToList();
                case 4:
                    List<UserAppointment> userAppointments =
                        _fakeUserAppointments
                        .Where(ua => ua.User == userId)
                        .ToList();

                    List<Guid> appId = new List<Guid>();

                    foreach (var item in userAppointments)
                    {
                        appId.Add(item.Appointment);
                    }

                    return _fakeDocuments
                        .Where(d => d.Appointment != null && appId.Contains((Guid)d.Appointment))
                        .ToList();
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<bool> Save()
        {
            return true;
        }

        public async Task<bool> UploadDocument(Guid? appointmentId, string title)
        {
            var document = new Document()
            {
                DocumentId = new Guid(),
                Title = title,
                Appointment = appointmentId
            };

            _fakeDocuments.Add(document);
            return await Save();
        }

        public async Task<bool> UploadFile(IFormFile file, string title)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> UploadFile2(IFormFile file, string title)
        {
            throw new NotImplementedException();
        }
    }
}
