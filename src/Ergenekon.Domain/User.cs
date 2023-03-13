using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Domain
{
    public class User : IdentityUser<int>
    {
        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class Role : IdentityRole<int>
    {

    }

    public static class SystemRoles
    {
        public const string Admin = "Admin";
        //public const string ReportViewer = "ReportViewer";
    }
}
