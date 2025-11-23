using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace OvertimeManager.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IUserContext, UserContext>();

            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();


            services.AddScoped<IPasswordHasher, PasswordHasher>();
            ///
            ////dynamicze wywołanie automappera aby moć użyć konstruktora bez parametrów
            //services.AddScoped(provider => new MapperConfiguration(cfg =>
            //{
            //    var scope = provider.CreateScope();
            //    var userContext = scope.ServiceProvider.GetRequiredService<IEmp>();
            //    cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            //}).CreateMapper());

            //services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
            //    .AddFluentValidationAutoValidation()
            //    .AddFluentValidationClientsideAdapters();
        }
    }
}
