using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IAppointmentRepository
    {
        //получение списка заявок мастером/управляющим/администратором
        Task<ICollection<UserAppointment>> GetAppointments(int roleId = 4);

        //создание заявки

        //изменение заявки создателем

        //изменение заявки руководителем

        //изменение заявки мастером

        //удаление заявки
    }
}
