
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OvertimeManager.Application.Extensions;
using OvertimeManager.Infrastructure.Extensions;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //add servicess from sub projects
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddDbContext<OvertimeManagerDbContext>(options =>
                options.UseSqlServer(builder.Configuration
                .GetConnectionString("OvertimeManagerConnectionString")));


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
