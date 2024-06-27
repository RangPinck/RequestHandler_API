using RequestHandler.Interfaces;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.xTest.FakeDate
{
    public class FakeRoleRepository : IRolesRepository
    {
        private readonly List<Role> _fakeRoles;
        public FakeRoleRepository()
        {
            _fakeRoles = new List<Role>()
            {
                new Role()
                {
                    RoleId = 1,
                    Title = "Администратор",
                },
                new Role()
                {
                    RoleId = 2,
                    Title = "Мастер",
                },
                new Role()
                {
                    RoleId = 3,
                    Title = "Руководитель",
                },
                new Role()
                {
                    RoleId = 4,
                    Title = "Пользователь",
                }
            };
        }
        public async Task<ICollection<Role>> GetRoles()
        {
            return _fakeRoles.OrderBy(r => r.RoleId).ToList();
        }
    }
}
