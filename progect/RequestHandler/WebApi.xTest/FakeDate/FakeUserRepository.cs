using RequestHandler.Interfaces;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.xTest.FakeDate
{
    internal class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _fakeUsers;

        public FakeUserRepository()
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
        }

        public Task<User> Authorithation(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUser(string login, string password, string surname, string name, int role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetAllUsers(int? roleId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(Guid id, string? login = null, string? password = null, string? surname = null, string? name = null, int? role = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAdmin(Guid logUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateApproval(Guid logUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateMaster(Guid logUserId)
        {
            throw new NotImplementedException();
        }
    }
}
