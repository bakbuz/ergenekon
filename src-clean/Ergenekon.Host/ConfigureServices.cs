using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Host.Services;
using Ergenekon.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace Ergenekon.Host;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        //services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers();

        // FluentValidation
        //services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        //{
        //    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
        //    var loggerFactory = provider.GetService<ILoggerFactory>();

        //    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        //});

        //services.AddFluentValidationAutoValidation(config =>
        //{
        //    config.DisableDataAnnotationsValidation = true;

        //}).AddFluentValidationClientsideAdapters(cfg=> {. });

        //services.AddFluentValidation(cfg => { cfg.LocalizationEnabled = true; });

        //services.AddFluentValidation();
        //services.AddValidatorsFromAssemblyContaining<SignUpCommandValidator>();

        services.AddFluentValidationAutoValidation();
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
        ValidatorOptions.Global.LanguageManager.Enabled = true;



        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[]{}
                }
            });
        });

        return services;
    }
}
