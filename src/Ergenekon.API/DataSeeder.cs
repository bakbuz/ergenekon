using Ergenekon.Domain;
using Ergenekon.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.API
{
    internal static class DataSeeder
    {
        private static DataContext _context;
        private static IWebHostEnvironment _env;

        internal static void Initialize(IServiceProvider serviceProvider)
        {
            _context = (DataContext)serviceProvider.GetRequiredService(typeof(DataContext));

            var ensureCreated = _context.Database.EnsureCreated();
            if (ensureCreated)
            {
                _env = (IWebHostEnvironment)serviceProvider.GetRequiredService(typeof(IWebHostEnvironment));

                var userManager = (UserManager<User>)serviceProvider.GetRequiredService(typeof(UserManager<User>));
                var roleManager = (RoleManager<Role>)serviceProvider.GetRequiredService(typeof(RoleManager<Role>));

                CreateUsers(userManager, roleManager);
                CreateCountries();
            }
        }


        private static void CreateUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var adminUser = new User();
            adminUser.UserName = "deli.dumrul@maydere.com";
            adminUser.Email = "deli.dumrul@maydere.com";
            adminUser.EmailConfirmed = true;
            adminUser.CreatedAt = DateTime.UtcNow;

            userManager.CreateAsync(adminUser, "Ab123,,").Wait();

            // roller
            var adminRole = new Role();
            adminRole.Name = SystemRoles.Admin;
            adminRole.NormalizedName = SystemRoles.Admin.ToUpper();
            adminRole.ConcurrencyStamp = Guid.NewGuid().ToString();

            if (roleManager != null)
            {
                roleManager.CreateAsync(adminRole).Wait();
            }
            else
            {
                _context.Roles.Add(adminRole);
                _context.SaveChanges();
            }

            userManager.AddToRoleAsync(adminUser, adminRole.Name).Wait();

            // other users
            var otherUsers = new List<string>();
            otherUsers.Add("bamsi.beyrek@maydere.com");
            otherUsers.Add("dede.korkut@maydere.com");
            otherUsers.Add("salur.kazan@maydere.com");
            otherUsers.Add("bani.cicek@maydere.com");
            foreach (var u in otherUsers)
            {
                var usr = new User();
                usr.UserName = u;
                usr.Email = u;
                usr.EmailConfirmed = true;
                usr.CreatedAt = DateTime.UtcNow;

                userManager.CreateAsync(usr, "Ab123,,").Wait();
                userManager.AddToRoleAsync(usr, adminRole.Name).Wait();
            }
        }

        private static void CreateCountries()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "App_Data\\Install\\CountriesAndStates.js");

            string jsonData;
            using (var r = new StreamReader(filePath))
            {
                jsonData = r.ReadToEnd();
            }

            var countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(jsonData).OrderBy(o => o.DisplayOrder).ToList();
            foreach (var c in countries)
            {
                _context.Set<Country>().Add(c);
                _context.SaveChanges();
            }
        }

    }
}