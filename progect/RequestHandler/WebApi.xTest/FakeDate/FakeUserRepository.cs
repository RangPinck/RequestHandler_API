using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RequestHandler.Another;
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

        public async Task<User> Authorithation(string login, string password)
        {
            string hash = Hashing.ToSHA256(password);
            return _fakeUsers.FirstOrDefault(u => u.Login == login && u.Password == hash);
        }

        public async Task<bool> CreateUser(string login, string password, string surname, string name, int role)
        {
            User user = new User()
            {
                UserId = Guid.NewGuid(),
                Login = login,
                Password = Hashing.ToSHA256(password),
                Surname = surname,
                Name = name,
                Role = role,
            };
            _fakeUsers.Add(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            User user = _fakeUsers.FirstOrDefault(u => u.UserId == id);
            _fakeUsers.Remove(user);
            return await Save();
        }

        public async Task<ICollection<User>> GetAllUsers(int? roleId = null)
        {
            List<User> users;

            if (roleId != null)
               return _fakeUsers.Where(u => u.Role == (int)roleId).ToList();
            else
                return _fakeUsers.ToList();
        }

        public async Task<bool> Save()
        {
            return true;
        }

        public async Task<bool> UpdateUser(Guid id, string? login = null, string? password = null, string? surname = null, string? name = null, int? role = null)
        {
            int index = _fakeUsers.FindIndex(u => u.UserId == id);
            if (index != -1)
            {
                var user = _fakeUsers.FirstOrDefault(u => u.UserId == id);

                user.Login = string.IsNullOrEmpty(login) ? user.Login : login;
                user.Password = string.IsNullOrEmpty(password) ? user.Password : Hashing.ToSHA256(password);
                user.Surname = string.IsNullOrEmpty(surname) ? user.Surname : surname;
                user.Name = string.IsNullOrEmpty(name) ? user.Name : name;
                if (role != null && role >= 1 && role <= 4)
                {
                    user.Role = (int)role;
                }
                return await Save();
            }
            return false;
        }

        public async Task<bool> UserExists(string login)
        {
            return _fakeUsers.Any(u => u.Login == login);
        }

        public async Task<bool> UserExists(Guid UserId)
        {
            return _fakeUsers.Any(u => u.UserId == UserId);
        }

        public async Task<bool> ValidateAdmin(Guid logUserId)
        {
            if (_fakeUsers
                .First(u => u.UserId == logUserId).Role == 1)
                return true;

            return false;
        }

        public async Task<bool> ValidateApproval(Guid logUserId)
        {
            if (_fakeUsers
               .First(u => u.UserId == logUserId).Role == 3)
                return true;

            return false;
        }

        public async Task<bool> ValidateMaster(Guid logUserId)
        {
            if (_fakeUsers
               .First(u => u.UserId == logUserId).Role == 2)
                return true;

            return false;
        }
    }
}
