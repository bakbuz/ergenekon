using Ergenekon.Application.Territory.Services;
using Ergenekon.Domain.Consts;
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
    private readonly IBasarsoftTerritoryService _basarsoftTerritoryService;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IWebHostEnvironment env,
        IBasarsoftTerritoryService basarsoftTerritoryService)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _env = env;
        _basarsoftTerritoryService = basarsoftTerritoryService;
    }

    public bool EnsureCreated()
    {
        return _context.Database.EnsureCreated();
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
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
        var administrator = new ApplicationUser { UserName = IdentityConsts.DefaultUserName, Email = IdentityConsts.DefaultUserEmail, CreatedAt = DateTime.Now };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, IdentityConsts.DefaultUserPass);
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

        // Territory items
        //if (!_context.Countries.Any())
        //CreateCountries();
    }

    private void CreateCountriesOld()
    {
        var countries = ReadFromJson<List<Country>>("CountriesAndStates.js")
            .OrderBy(o => o.DisplayOrder)
            .ToList();

        var ciTR = new System.Globalization.CultureInfo("tr-TR");
        var ciEN = new System.Globalization.CultureInfo("en-US");

        foreach (var country in countries)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        CreateDistricts();
    }

    private void CreateCountries()
    {
        var countries = ReadFromJson<List<Country>>("CountriesAndStates.js")
            .OrderBy(o => o.DisplayOrder)
            //.Reverse()
            .ToList();

        //var ciTR = new System.Globalization.CultureInfo("tr-TR");
        //var ciEN = new System.Globalization.CultureInfo("en-US");

        foreach (var country in countries)
        {
            //country.Name = country.Name.ToUpper(ciTR);
            //country.EnglishName = country.EnglishName.ToUpper(ciEN);

            //foreach (var province in country.Provinces)
            //{
            //province.Name = province.Name.ToUpper();
            //}

            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        //CreateDistricts();
        CreateDistrictsFromBasarsoft();
    }

    private void CreateDistricts()
    {
        var trCountryId = _context.Countries.Single(s => s.Iso2Code == "TR").Id;
        var trProvinces = _context.Provinces.Where(q => q.CountryId == trCountryId).ToList();

        var allProvinces = ReadFromJson<List<CityImportDto>>("turkiye_il_ilce.json")
            .OrderBy(o => o.DisplayOrder)
            .ToList();

        foreach (var sp in trProvinces)
        {
            var province = allProvinces.Where(q => q.Abbreviation == sp.Abbreviation).Single();
            var districtNames = province.Districts.Order();

            foreach (var districtName in districtNames)
            {
                var district = new District();
                district.ProvinceId = sp.Id;
                district.Name = districtName;

                _context.Districts.Add(district);
                _context.SaveChanges();
            }
        }
    }

    private void CreateDistrictsFromBasarsoft()
    {
        var trCountryId = _context.Countries.Single(s => s.Iso2Code == "TR").Id;
        var trProvinces = _context.Provinces.Where(q => q.CountryId == trCountryId).ToList();

        var bsDistricts = _basarsoftTerritoryService.GetDistricts();
        //_context.Districts.AddRange(bsDistricts);
        //_context.SaveChanges();

        var bsNeighborhoods = _basarsoftTerritoryService.GetNeighborhoods();
        //_context.Neighborhoods.AddRange(bsNeighborhoods);
        //_context.SaveChanges();

        foreach (var province in trProvinces)
        {
            var districts = bsDistricts.Where(q => q.ProvinceId == province.Id).ToList();
            foreach (var district in districts)
            {
                var bsDistrictId = district.Id;
                district.Id = 0;

                _context.Districts.Add(district);
                _context.SaveChanges();

                var neighborhoods = bsNeighborhoods.Where(q => q.DistrictId == bsDistrictId).ToList();
                foreach (var neighborhood in neighborhoods)
                {
                    neighborhood.Id = 0;
                    neighborhood.DistrictId = district.Id;

                    _context.Neighborhoods.Add(neighborhood);
                    _context.SaveChanges();
                }
            }
        }
    }

    #region utils

    private T ReadFromJson<T>(string fileName)
    {
        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "Install", fileName);

        string jsonData;
        using (var r = new StreamReader(filePath))
        {
            jsonData = r.ReadToEnd();
        }

        return JsonSerializer.Deserialize<T>(jsonData);
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
