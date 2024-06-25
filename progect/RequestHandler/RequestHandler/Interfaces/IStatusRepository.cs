using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IStatusRepository
    {
        //получение списка статусов
        Task<ICollection<Status>> GetStatuses();
    }
}
