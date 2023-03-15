using Ergenekon.API;
using Ergenekon.Domain;
using Ergenekon.Infrastructure;
using Ergenekon.Services;
using Ergenekon.Services.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// identity
builder.Services.AddIdentity<User, Role>(opts =>
    {
        opts.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// email
builder.Services.AddSingleton<IEmailSender, NullEmailSender>();

// Add services to the container.
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateProvinceService, StateProvinceService>();


//builder.Services.AddAutoMapper(typeof(Ergenekon.Services.ICountryService), typeof(ProfileTypeFromAssembly2) /*, ...*/);
builder.Services.AddAutoMapper(typeof(CountryService));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    DataSeeder.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();