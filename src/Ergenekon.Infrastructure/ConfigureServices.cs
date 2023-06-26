using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.World.Services;
using Ergenekon.Infrastructure.Files;
using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Localization;
using Ergenekon.Infrastructure.Persistence;
using Ergenekon.Infrastructure.Persistence.Interceptors;
using Ergenekon.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ErgenekonDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<TurkishIdentityErrorDescriber>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;

            // Lockout settings.
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        });

        //services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<ICsvFileBuilder, CsvFileBuilder>();
        services.AddScoped<Ergenekon.Infrastructure.Identity.IAuthenticationService, Ergenekon.Infrastructure.Identity.AuthenticationService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IWorldService, WorldService>();
        services.AddScoped<IMailboxService, MailboxService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        // email
        services
            .AddFluentEmail("postaci@maydere.com", "Postacı")
            .AddSmtpSender("smtp.yandex.com.tr", 587, "postaci@maydere.com", "bYPdPPgzSXGAw7P");

        return services;
    }
}
