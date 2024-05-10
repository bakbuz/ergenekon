using Ergenekon.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
