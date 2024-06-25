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

        //проверка наличия пользователя
        public async Task<bool> UserExists(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
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
    }
}
