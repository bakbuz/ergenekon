using Ergenekon.Infrastructure.Data;
using Ergenekon.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHostServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
//app.UseStaticFiles();

// Identity Endpoints
app.MapGroup("account").MapIdentityApi<ApplicationUser>().WithTags("Account");

// NSwag
app.UseOpenApi();   // serve OpenAPI/Swagger documents
app.UseSwaggerUi(); // serve Swagger UI
app.UseReDoc();     // serve ReDoc UI

app.MapControllers();
//app.MapRazorPages();

//app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

//app.Map("/", () => Results.Redirect("/swagger"));
//app.MapEndpoints();

app.Run();

public partial class Program { }