using Microsoft.EntityFrameworkCore;
using RequestHandler.Interfaces;
using RequestHandler.Models;

namespace RequestHandler.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly RequestHandlerContext _context;

        public AppointmentRepository(RequestHandlerContext context)
        {
            _context = context;
        }

        //получение списка заявок мастером/управляющим/администратором
        public async Task<ICollection<UserAppointment>> GetAppointments(int roleId = 4)
        {
            return await _context.UserAppointments
                .Include(ua => ua.UserNavigation)
                .Include(ua => ua.AppointmentNavigation)
                .Include(ua => ua.AppointmentNavigation.Documents)
                .Where(ua => ua.UserNavigation.Role == roleId)
                .ToListAsync();
        }

        //создание заявки

        //изменение заявки создателем

        //изменение заявки руководителем

        //изменение заявки мастером

        //удаление заявки
    }
}
