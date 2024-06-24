using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IUserRepository
    {
        //проверка наличия пользователя
        Task<bool> UserExists(Guid logUserId);

        //проверка на администратора
        Task<bool> ValidateAdmin(Guid logUserId);

        //проверка на управляющего
        Task<bool> ValidateApproval(Guid logUserId);

        //проверка на мастера
        Task<bool> ValidateMaster(Guid logUserId);

        //получние списка всех пользователей
        //если надо будет, по поменять тип списка данных
        Task<ICollection<User>> GetAllUsers(int? roleId = null);

        //авторизация
        //если надо будет, по поменять тип списка данных
        Task<User> Authorithation(string login, string password);
    }
}
