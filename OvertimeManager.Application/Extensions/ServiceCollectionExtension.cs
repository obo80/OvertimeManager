using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IUserContext, UserContext>();

            //services.AddMediatR(cfg =>
            //    cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarWorkshopCommand)));

            ////services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
            ////dynamicze wywołanie automappera aby moć użyć konstruktora bez parametrów
            //services.AddScoped(provider => new MapperConfiguration(cfg =>
            //{
            //    var scope = provider.CreateScope();
            //    var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
            //    cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            //}).CreateMapper());

            //services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
            //    .AddFluentValidationAutoValidation()
            //    .AddFluentValidationClientsideAdapters();
        }
    }
}
