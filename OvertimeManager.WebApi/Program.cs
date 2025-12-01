
using Microsoft.EntityFrameworkCore;
using OvertimeManager.Api.Extensions;
using OvertimeManager.Api.Middlewares;
using OvertimeManager.Application.Extensions;
using OvertimeManager.Infrastructure.Extensions;
using OvertimeManager.Infrastructure.Persistence;
using OvertimeManager.Infrastructure.Seeders;

namespace OvertimeManager.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //add servicess from sub projects
            builder.AddPresentation();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddDbContext<OvertimeManagerDbContext>(options =>
                options.UseSqlServer(builder.Configuration
                .GetConnectionString("OvertimeManagerConnectionString")));


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<OvertimeManagerSeeder>();
            await seeder.Seed();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
