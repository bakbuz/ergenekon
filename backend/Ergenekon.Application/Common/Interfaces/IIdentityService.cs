using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Users.Shared;

namespace Ergenekon.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserSummaryDto?> GetUserAsync(string userId, CancellationToken cancellationToken);

    Task<string?> GetUsernameAsync(string userId, CancellationToken cancellationToken);

    Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken);

    Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellationToken);

    Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken);
}
