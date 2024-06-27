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

        //создание заявки (после получаю последний id заявки для закрепления документа)
        public async Task<bool> CreateAppointment(Guid userId, string Problem, string? DiscriptionProblem, string Place)
        {
            Guid appId = Guid.NewGuid();
            
            Appointment appointment = new Appointment()
            {
                AppointmentId = appId,
                Problem = Problem,
                DiscriptionProblem = DiscriptionProblem,
                Place = Place,
                DateCreate = DateTime.UtcNow
            };

            await _context.Appointments.AddAsync(appointment);

            var sv = await Save();

            await _context.Database.ExecuteSqlAsync($"INSERT INTO [User_appointment] ([appointment], [user]) VALUES({appId},{userId})");

            return sv;
        }

        //удаление заявки
        public async Task<bool> DeleteAppointment(Guid appointmentId)
        {
            var app = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
            _context.Appointments.Remove(app);
            return await Save();
        }

        //получение списка заявок мастером/управляющим/администратором
        public async Task<ICollection<UserAppointment>> GetAppointments(int roleId = 4)
        {
            if (roleId > 4 || roleId < 1)
            {
                return await _context.UserAppointments
                .Include(ua => ua.UserNavigation)
                .Include(ua => ua.AppointmentNavigation)
                .Include(ua => ua.AppointmentNavigation.StatusNavigation)
                .ToListAsync();
            }

            return await _context.UserAppointments
                .Include(ua => ua.UserNavigation)
                .Include(ua => ua.AppointmentNavigation)
                .Include(ua => ua.AppointmentNavigation.StatusNavigation)
                .Where(ua => ua.UserNavigation.Role == roleId)
                .ToListAsync();
        }

        //изменение заявки создателем
        public async Task<bool> UpdateAppointment(Guid appointmentId, string? Problem, string? DiscriptionProblem, string? Place)
        {
            var app = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            app.Problem = string.IsNullOrEmpty(Problem) ? app.Problem : Problem;
            
            app.DiscriptionProblem = string.IsNullOrEmpty(DiscriptionProblem) ? app.DiscriptionProblem : DiscriptionProblem;
            app.Place = string.IsNullOrEmpty(Place) ? app.Place : Place;
           
            _context.Appointments.Update(app);

            return await Save();
        }

        //изменение заявки руководителем
        public async Task<bool> UpdateAppointmentApprove(Guid userId, Guid appointmentId)
        {
            var app = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            app.DateApprove = DateTime.UtcNow;
            app.Status = 2;

            _context.Appointments.Update(app);

            var sv = await Save();

            await _context.Database.ExecuteSqlAsync($"INSERT INTO [User_appointment] ([appointment], [user]) VALUES({appointmentId},{userId})");

            return sv;
        }

        //изменение заявки мастером
        public async Task<bool> UpdateAppointmentFix(Guid userId, Guid appointmentId)
        {
            var app = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            app.DateFix = DateTime.UtcNow;
            app.Status = 3;

            _context.Appointments.Update(app);

            var sv = await Save();

            await _context.Database.ExecuteSqlAsync($"INSERT INTO [User_appointment] ([appointment], [user]) VALUES({appointmentId},{userId})");

            return sv;
        }

        //сохранение результата
        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        //проверка наличия заявки по id
        public async Task<bool> AppointmentExists(Guid appointmentId)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == appointmentId);
        }
    }
}
