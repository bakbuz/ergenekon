using Ergenekon.Application.Common.Behaviours;
using Ergenekon.Application.Storage;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Minio;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });

        ValidatorOptions.Global.LanguageManager.Enabled = true;
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");

        services.AddMinio(cfg =>
        {
            cfg.WithEndpoint(configuration["MinioOptions:Endpoint"]);
            cfg.WithCredentials(configuration["MinioOptions:AccessKey"], configuration["MinioOptions:SecretKey"]);
            cfg.WithSSL(configuration["MinioOptions:UseSSL"] == "true");
        });
        services.AddScoped<IFileStorage, FileStorage>();

        return services;
    }
}
