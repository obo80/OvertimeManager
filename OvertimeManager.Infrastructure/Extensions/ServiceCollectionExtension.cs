using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Authentication;
using OvertimeManager.Infrastructure.Persistence;
using OvertimeManager.Infrastructure.Repositories;
using OvertimeManager.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authentication;
using OvertimeManager.Application.Common;

namespace OvertimeManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OvertimeManagerConnectionString");
            services.AddDbContext<OvertimeManagerDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<OvertimeManagerSeeder>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOvertimeRepository, OvertimeRepository>();
            services.AddScoped<ICompensationRepository, CompensationRepository>();


            services.AddScoped<IJwtService, JwtService>();
            #region Authentication
            //authentication settings
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            #endregion

            
        }
    }
}
