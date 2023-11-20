using Dapper;
using Ergenekon.Application.Territory.Services;
using Ergenekon.Domain.Common;
using Ergenekon.Domain.Consts;
using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ergenekon.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _env;
    private readonly IBasarsoftTerritoryService _basarsoftTerritoryService;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        IConfiguration configuration,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IWebHostEnvironment env,
        IBasarsoftTerritoryService basarsoftTerritoryService)
    {
        _logger = logger;
        _configuration = configuration;
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
        if (!_context.Countries.Any())
            await CreateCountriesAsync();
    }

    private async Task CreateCountriesAsync()
    {
        var data = ReadFromJson<List<Country>>("CountriesAndStates.js")
            .OrderBy(o => o.DisplayOrder)
            .ToList();

        var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();

        foreach (var item in data)
        {
            //var country = new Country();
            //country.Name = item.Name;
            //country.EnglishName = item.EnglishName;
            //country.Iso2Code = item.Iso2Code;
            //country.Iso3Code = item.Iso3Code;
            //country.NumericIsoCode = item.NumericIsoCode;
            //country.CallingCode = item.CallingCode;
            //country.Published = item.Published;
            //country.DisplayOrder = item.DisplayOrder;

            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();

            byte countryId = await con.QuerySingleAsync<byte>("INSERT INTO [Territory].[Countries] ([Name],[EnglishName],[Iso2Code],[Iso3Code],[NumericIsoCode],[CallingCode],[Published],[DisplayOrder]) VALUES(@Name,@EnglishName,@Iso2Code,@Iso3Code,@NumericIsoCode,@CallingCode,@Published,@DisplayOrder); SELECT SCOPE_IDENTITY();", item);
            //var countryId = Convert.ToByte(newId);

            var provinces = item.Provinces.ToList();
            foreach (var province in provinces)
            {
                province.CountryId = countryId;
            }
            await con.ExecuteAsync("INSERT INTO [Territory].[Provinces] ([CountryId],[Name],[Abbreviation],[DisplayOrder]) VALUES(@CountryId,@Name,@Abbreviation,@DisplayOrder)", provinces);
        }

        con.Close();
        con.Dispose();

        //CreateDistricts();
        //CreateDistrictsFromBasarsoft();
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

    public class CountryImport : BaseEntity<byte>
    {
        public CountryImport()
        {
            Provinces = new HashSet<ProvinceImport>();
        }

        public string Name { get; set; } = null!;

        public string EnglishName { get; set; } = null!;

        public string Iso2Code { get; set; } = null!;

        public string Iso3Code { get; set; } = null!;

        public short NumericIsoCode { get; set; }

        public short? CallingCode { get; set; }

        public bool Published { get; set; }

        public short DisplayOrder { get; set; }

        public virtual ICollection<ProvinceImport> Provinces { get; set; }
    }

    public class ProvinceImport : BaseEntity<ushort>
    {
        public byte CountryId { get; set; }

        public string Name { get; set; } = null!;

        public string Abbreviation { get; set; } = null!;

        public short DisplayOrder { get; set; }
    }

    #endregion

}
