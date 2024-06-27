using Microsoft.AspNetCore.Mvc;
using RequestHandler.Models;

namespace RequestHandler.Interfaces
{
    public interface IDocumentRepository
    {
        //проверка наличия документа по id
        Task<bool> DocumentExists(Guid doucumentId);

        //проверка наличия документа по названию
        Task<bool> DocumentExists(string title);

        //проверка наличия документов по заявке
        Task<bool> DocumentExistsOfAppointment(Guid appointmentId);

        //получение списка всех документов
        Task<ICollection<Document>> GetDocuments(Guid userId);

        //добаление докуменетов
        Task<bool> UploadDocument(Guid? appointmentId, string title);

        //запись файла
        Task<bool> UploadFile(IFormFile file, string title);

        //получение файла
        Task<FileContentResult> DownloadFile(string title);

        //удаление документа
        Task<bool> DeleteDocument(Guid doucumentId);

        //удаление файла
        Task<bool> DeleteFile(Guid doucumentId);

        //сохранение результата
        Task<bool> Save();

        Task<bool> UploadFile2(IFormFile file, string title);

    }
}
