using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();

        bool CreteUser(User user);

        bool DeleteUser();

        bool UpdateUser();
    }
}
