using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Pages.Services;
using Ergenekon.Application.SearchTerms.Services;
using Ergenekon.Application.World.Services;
using Ergenekon.Infrastructure.Files;
using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Localization;
using Ergenekon.Infrastructure.Persistence;
using Ergenekon.Infrastructure.Persistence.Interceptors;
using Ergenekon.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

        services.AddScoped<ICsvFileBuilder, CsvFileBuilder>();
        services.AddScoped<Ergenekon.Infrastructure.Identity.IAuthenticationService, Ergenekon.Infrastructure.Identity.AuthenticationService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IWorldService, WorldService>();
        services.AddScoped<IMailboxService, MailboxService>();

        services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<ISearchTermService, SearchTermService>();
        services.AddScoped<IBasarsoftTerritoryService, BasarsoftTerritoryService>();        

        //services.AddAuthentication().AddIdentityServerJwt();
        services.AddAuthenticationJwt(configuration);

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        // email
        services
            .AddFluentEmail("postaci@maydere.com", "Postacı")
            .AddSmtpSender("smtp.yandex.com.tr", 587, "postaci@maydere.com", "bYPdPPgzSXGAw7P");

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = true;
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");

        return services;
    }
}

public static class Ext
{
    public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,    // İzin verilecek sitelerin denetlenip denetlenmeyeceğini belirler
                    ValidateIssuer = true,      // 
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidAudience = configuration["JwtOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"])),
                    ClockSkew = TimeSpan.Zero // sunucular arasındaki zaman farkını sıfırlamak için
                };
            });

        return services;
    }
}
