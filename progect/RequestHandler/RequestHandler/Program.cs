
using Microsoft.EntityFrameworkCore;
using RequestHandler.Models;

namespace RequestHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<RequestHandlerContext>(
                options =>
                {
                    options.UseSqlServer(

                        //�� "�� 70-����� ������"
                        //builder.Configuration.GetConnectionString("AtTheFactory")
                        //"���������"
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
