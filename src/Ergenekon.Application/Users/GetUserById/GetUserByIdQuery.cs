using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Users.Shared;
using MediatR;

namespace Ergenekon.Application.Users.GetUserById;

public record GetUserByIdQuery() : IRequest<UserSummaryDto?>
{
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserSummaryDto?>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUser _currentUser;

    public GetUserByIdQueryHandler(IIdentityService identityService, ICurrentUser currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<UserSummaryDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        string? userId = _currentUser.UserId;
        if (string.IsNullOrEmpty(userId))
            return null;

        return await _identityService.GetUserAsync(userId, cancellationToken);
    }
}
