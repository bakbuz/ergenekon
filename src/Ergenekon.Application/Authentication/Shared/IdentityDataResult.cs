using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Application.Authentication.Shared;

public class IdentityDataResult : IdentityResult
{
    public string UserId { get; set; }
}
