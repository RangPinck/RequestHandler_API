using Microsoft.EntityFrameworkCore;
using RequestHandler.Another;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RequestHandlerContext _context;

        public UserRepository(RequestHandlerContext context)
        {
            _context = context;
        }

        //проверка наличия пользователя по логину
        public async Task<bool> UserExists(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
        }

        //проверка наличия пользователя по id
        public async Task<bool> UserExists(Guid UserId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == UserId);
        }

        //получние списка всех пользователей
        public async Task<ICollection<User>> GetAllUsers(int? roleId = null)
        {
            List<User> users;

            if (roleId != null)
                users = _context.Users.Where(u => u.Role == (int)roleId).
                    Include(r => r.RoleNavigation).ToList();
            else
                users = _context.Users.
                    Include(r => r.RoleNavigation).ToList();

            return users.OrderBy(r => r.RoleNavigation.RoleId).ToList();
        }

        //проверка на администратора
        public async Task<bool> ValidateAdmin(Guid logUserId)
        {
            if (_context.Users
                .FirstAsync(u => u.UserId == logUserId)
                    .Result.Role == 1)
                return true;

            return false;
        }

        //проверка на мастера
        public async Task<bool> ValidateMaster(Guid logUserId)
        {
            if (_context.Users
                .FirstAsync(u => u.UserId == logUserId)
                    .Result.Role == 2)
                return true;

            return false;
        }

        //проверка на управляющего
        public async Task<bool> ValidateApproval(Guid logUserId)
        {
            if (_context.Users
                .FirstAsync(u => u.UserId == logUserId)
                    .Result.Role == 3)
                return true;

            return false;
        }

        //авторизация
        public async Task<User> Authorithation(string login, string password)
        {
            string hexPassword = Hashing.ToSHA256(password);
            return await _context.Users
                .Include(r => r.RoleNavigation)
                .FirstOrDefaultAsync(u => u.Login == login
                    && u.Password == hexPassword);
        }

        //создание пользователя
        public async Task<bool> CreateUser(string login, string password, string surname, string name, int role)
        {
            password = Hashing.ToSHA256(password);

            User user = new User()
            {
                UserId = new Guid(),
                Login = login,
                Password = password,
                Surname = surname,
                Name = name,
                Role = role
            };

            _context.Users.Add(user);

            return await Save();
        }

        //сохранение результата
        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        //обновление пользователя
        public async Task<bool> UpdateUser(Guid id, string? login = null, string? password = null, string? surname = null, string? name = null, int? role = null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            user.Login = string.IsNullOrEmpty(login) ? user.Login : login;
            user.Password = string.IsNullOrEmpty(password) ? user.Password : Hashing.ToSHA256(password);
            user.Surname = string.IsNullOrEmpty(surname) ? user.Surname : surname;
            user.Name = string.IsNullOrEmpty(name) ? user.Name : name;
            if (role != null && role >=1 && role <= 4)
            {
                user.Role = (int)role;
            }

            _context.Users.Update(user);
            return await Save();
        }

        //удаление пользователя администратором
        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            _context.Users.Remove(user);
            return await Save();
        }
    }
}
