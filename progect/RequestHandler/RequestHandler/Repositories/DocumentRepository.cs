using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using System.IO;

namespace RequestHandler.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly RequestHandlerContext _context;

        public DocumentRepository(RequestHandlerContext context)
        {
            _context = context;
        }

        //проверка наличия документа по id
        public async Task<bool> DocumentExists(Guid doucumentId)
        {
            return await _context.Documents.AnyAsync(d => d.DocumentId == doucumentId);
        }

        //проверка наличия документа по названию
        public async Task<bool> DocumentExists(string title)
        {
            return await _context.Documents.AnyAsync(d => d.Title == title);
        }

        //проверка наличия документов по заявке
        public async Task<bool> DocumentExistsOfAppointment(Guid appointmentId)
        {
            return await _context.Documents.AnyAsync(d => d.Appointment == appointmentId);
        }

        //получение списка всех документов
        public async Task<ICollection<Document>> GetDocuments(Guid userId)
        {
            User user = await _context.Users.FirstAsync(u => u.UserId == userId);

            switch (user.Role)
            {
                case < 4:
                    return await _context.Documents.ToListAsync();
                case 4:
                    List<UserAppointment> userAppointments =
                        await _context.UserAppointments
                        .Where(ua => ua.User == userId)
                        .ToListAsync();

                    List<Guid> appId = new List<Guid>();

                    foreach (var item in userAppointments)
                    {
                        appId.Add(item.Appointment);
                    }

                    return await _context.Documents
                        .Where(d => d.Appointment != null && appId.Contains((Guid)d.Appointment))
                        .ToListAsync();
                default:
                    throw new NotImplementedException();
            }
        }

        //добаление докуменетов
        public async Task<bool> UploadDocument(Guid? appointmentId, string title)
        {
            var document = new Document()
            {
                DocumentId = new Guid(),
                Title = title,
                Appointment = appointmentId
            };

            await _context.Documents.AddAsync(document);
            return await Save();
        }

        //отправка файла на сервер
        public async Task<bool> UploadFile(IFormFile file, string title)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UploadFile2(IFormFile file, string title)
        {
            try
            {
                // Загрузка файла на сервер
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //удаление документа
        public async Task<bool> DeleteDocument(Guid doucumentId)
        {
            var doc = await _context.Documents.FirstAsync(
                d => d.DocumentId == doucumentId);

            _context.Documents.Remove(doc);
            return await Save();
        }

        //сохранение результата
        public async Task<bool> Save()
        {
            var save = await _context.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        //получение файла
        public async Task<FileContentResult> DownloadFile(string title)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return new FileContentResult(bytes, contenttype)
            {
                FileDownloadName = title
            };
        }

        //удаление файла
        public async Task<bool> DeleteFile(Guid doucumentId)
        {
            Document file = await _context.Documents.FirstAsync(d => d.DocumentId == doucumentId);
            string title = file.Title;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "LocalFiles", title);
            try
            {
                FileInfo fileInf = new FileInfo(filepath);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
