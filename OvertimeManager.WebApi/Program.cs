
using Microsoft.EntityFrameworkCore;
using OvertimeManager.Api.Extensions;
using OvertimeManager.Api.Middlewares;
using OvertimeManager.Application.Extensions;
using OvertimeManager.Infrastructure.Extensions;
using OvertimeManager.Infrastructure.Persistence;
using OvertimeManager.Infrastructure.Seeders;
using Serilog;

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
            //builder.Services.AddOpenApi();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<OvertimeManagerSeeder>();
            await seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSerilogRequestLogging();

            // Redirect root "/" to Swagger UI
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/" || string.IsNullOrEmpty(context.Request.Path.Value))
                {
                    context.Response.Redirect("/swagger");
                    return;
                }
                await next();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicWeb API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
