using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OvertimeManager.Api.Middlewares;
using OvertimeManager.Infrastructure.Authentication;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace OvertimeManager.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddControllers();


            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
            );
            builder.Services.AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                        },
                        []
                    }

                });
            });
        }

    }
}
