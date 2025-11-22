using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OvertimeManager.Infrastructure.Persistence;
using OvertimeManager.Infrastructure.Seeders;

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

            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<IOvertimeRequestRepository, OvertimeRequestRepository>();
            //services.AddScoped<IOvertimeCompensationRequestRepository, OvertimeCompensationRequestRepository>();
        }
    }
}
