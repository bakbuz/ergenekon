using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Users.Shared;

namespace Ergenekon.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserSummaryDto?> GetUserAsync(string userId, CancellationToken cancellationToken);

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}
