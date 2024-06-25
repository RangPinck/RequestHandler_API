using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IUserRepository
    {
        //проверка наличия пользователя по логину
        Task<bool> UserExists(string login);

        //проверка наличия пользователя по id
        Task<bool> UserExists(Guid UserId);

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

        //создание пользователя администратором
        Task<bool> CreateUser(string login, string password, string surname, string name, int role);

        //удаление пользователя администратором

        //обновление пользователя администратором
        Task<bool> UpdateUser(Guid id, string? login = null, string? password = null, string? surname = null, string? name = null, int? role = null);

        //сохранение результата
        Task<bool> Save();
    }
}
