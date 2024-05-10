using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Users.Shared;

namespace Ergenekon.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserSummaryDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken);

    Task<string?> GetUsernameAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken);

    Task<bool> AuthorizeAsync(Guid userId, string policyName, CancellationToken cancellationToken);

    Task<Result> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
}
