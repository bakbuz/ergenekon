using Ergenekon.Application.Storaging;
using Ergenekon.Host.Extensions;
using Ergenekon.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .WriteTo.Console()
    //.WriteTo.Seq("http://localhost:5341")
    .ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHostServices(builder.Configuration);

builder.Services.AddStackExchangeRedisCache(action =>
{
    action.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection("MinioOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialize and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        //await initializer.InitializeAsync();
        //await initializer.SeedAsync();

        // TEMP
        if (initializer.EnsureCreated())
        {
            await initializer.SeedAsync();
        }
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseRouting();
app.UseCors(Ergenekon.Host.App.DefaultCorsPolicyName);

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.UseSerilogRequestLogging(options =>
{
    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("QueryString", httpContext.Request.QueryString.ToString());
        diagnosticContext.Set("RequestBody", httpContext.ReadRequestBody());
        diagnosticContext.Set("ResponseBody", httpContext.ReadResponseBody());
    };
});

app.UseOpenApi();       // serve OpenAPI/Swagger documents
app.UseSwaggerUi3();    // serve Swagger UI
app.UseReDoc();         // serve ReDoc UI

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();