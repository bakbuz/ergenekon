using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
