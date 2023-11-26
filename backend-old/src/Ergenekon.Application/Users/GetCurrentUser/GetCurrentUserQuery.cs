using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Users.Shared;
using MediatR;

namespace Ergenekon.Application.Users.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<UserSummaryDto?>
{
}

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserSummaryDto?>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUser _currentUser;

    public GetCurrentUserQueryHandler(IIdentityService identityService, ICurrentUser currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<UserSummaryDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        string? userId = _currentUser.UserId;
        if (string.IsNullOrEmpty(userId))
            return null;

        return await _identityService.GetUserAsync(userId, cancellationToken);
    }
}
