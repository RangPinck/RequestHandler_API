using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IAppointmentRepository
    {
        //получение списка заявок мастером/управляющим/администратором
        Task<ICollection<UserAppointment>> GetAppointments(int roleId = 4);

        //создание заявки
        Task<bool> CreateAppointment(Guid userId, string Problem, string? DiscriptionProblem, string Place);

        //изменение заявки создателем
        Task<bool> UpdateAppointment(Guid appointmentId, string? Problem, string? DiscriptionProblem, string? Place);

        //изменение заявки руководителем
        Task<bool> UpdateAppointmentApprove(Guid userId, Guid appointmentId);

        //изменение заявки мастером
        Task<bool> UpdateAppointmentFix(Guid userId, Guid appointmentId);

        //удаление заявки
        Task<bool> DeleteAppointment(Guid appointmentId);

        //сохранение результата
        Task<bool> Save();
    }
}
