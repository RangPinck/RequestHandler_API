using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IUserRepository
    {
        //проверка наличия пользователя
        Task<bool> UserExists(string login);

        //проверка на администратора
        Task<bool> ValidateAdmin(Guid logUserId);

        //проверка на управляющего
        Task<bool> ValidateApproval(Guid logUserId);

        //проверка на мастера
        Task<bool> ValidateMaster(Guid logUserId);

        //получние списка всех пользователей
        Task<ICollection<User>> GetAllUsers(int? roleId = null);

        //авторизация
        Task<User> Authorithation(string login, string password);
    }
}
