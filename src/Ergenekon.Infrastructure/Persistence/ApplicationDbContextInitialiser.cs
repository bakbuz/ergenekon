using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ergenekon.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _env;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IWebHostEnvironment env)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _env = env;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                //await _context.Database.MigrateAsync();
                await _context.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "maydere", Email = "bayram@maydere.com", CreatedAt = DateTime.Now };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Ab123!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }

        // World items
        CreateCountries();
    }

    private void CreateCountries()
    {
        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "Install", "CountriesAndStates.js");
        string jsonData = ReadFile(filePath);

        var countries = JsonSerializer.Deserialize<List<Country>>(jsonData).OrderBy(o => o.DisplayOrder).ToList();
        foreach (var country in countries)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        CreateDistricts();
    }

    private void CreateDistricts()
    {
        var trCountryId = _context.Countries.Single(s => s.Iso2Code == "TR").Id;
        var trStateProvinces = _context.StateProvinces.Where(q => q.CountryId == trCountryId).ToList();

        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "Install", "turkiye_il_ilce.json");
        string jsonData = ReadFile(filePath);

        var allProvinces = JsonSerializer.Deserialize<List<CityImportDto>>(jsonData).OrderBy(o => o.DisplayOrder).ToList();

        foreach (var sp in trStateProvinces)
        {
            var province = allProvinces.Where(q => q.Abbreviation == sp.Abbreviation).Single();

            foreach (var d in province.Districts.Order())
            {
                var district = new District();
                district.StateProvinceId = sp.Id;
                district.Name = d;

                _context.Districts.Add(district);
                _context.SaveChanges();
            }
        }
    }

    #region utils

    private string ReadFile(string filePath)
    {
        string jsonData;
        using (var r = new StreamReader(filePath))
        {
            jsonData = r.ReadToEnd();
        }

        return jsonData;
    }

    internal class CityImportDto
    {
        [JsonPropertyName("mame")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; } = default!;

        [JsonPropertyName("display_order")]
        public int DisplayOrder { get; set; }

        [JsonPropertyName("district_count")]
        public int DistrictCount { get; set; }

        [JsonPropertyName("districts")]
        public List<string> Districts { get; set; } = default!;
    }

    #endregion
}
