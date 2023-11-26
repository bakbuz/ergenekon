using Ergenekon.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Identity;

public static class IdentityExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public static string GetDisplayName(this ApplicationUser user)
    {
        if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            return $"{user.FirstName} {user.LastName}";

        if (!string.IsNullOrEmpty(user.FirstName))
            return user.FirstName;

        if (!string.IsNullOrEmpty(user.LastName))
            return user.LastName;

        return user.UserName!;
    }
}
