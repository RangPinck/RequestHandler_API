using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RequestHandler.Interfaces;
using RequestHandler.Models;
using RequestHandler.Repositories;

namespace RequestHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IStatusRepository, StatusRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<RequestHandlerContext>(
                options =>
                {
                    options.UseSqlServer(

                        //АО "НЗ 70-летия Победы"
                        //builder.Configuration.GetConnectionString("AtTheFactory")
                        //"локальная"
                        builder.Configuration.GetConnectionString("LocalDbFirst")
                        //builder.Configuration.GetConnectionString("LocalDbSecond")
                        );
                });
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            builder.Services.AddCors(
                opt =>
                {
                    opt.AddPolicy("Availability", builder =>
                    {
                        builder
                        //написать только доверенные порты
                        //.WithOrigins("","")
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                }
                );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseCors("Availability");
            app.MapControllers();
            app.Run();
        }
    }
}
