using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;
using OvertimeManager.Infrastructure.Repositories;
using OvertimeManager.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddScoped<IOvertimeRequestRepository, OvertimeRequestRepository>();
            services.AddScoped<IOvertimeCompensationRequestRepository, OvertimeCompensationRequestRepository>();
        }
    }
}
