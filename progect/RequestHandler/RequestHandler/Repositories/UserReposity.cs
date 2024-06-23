using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Repositories
{
    public class UserReposity:IUserRepository
    {
        private readonly RequestHandlerContext _ctx;

        public UserReposity(RequestHandlerContext context)
        {
            _ctx = context;
        }

        public bool CreteUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser()
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAllUsers()
        {
            return _ctx.Users.ToList();
        }

        public bool UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
