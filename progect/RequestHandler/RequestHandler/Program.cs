using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
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


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
