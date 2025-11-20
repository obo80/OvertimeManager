using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OvertimeManager.Application.CQRS.Employee.DTOs;
using OvertimeManager.Application.CQRS.Employee.Queries.GetAllEmployees;
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

            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();
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
