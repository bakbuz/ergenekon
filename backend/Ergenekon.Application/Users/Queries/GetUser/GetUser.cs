using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Users.Shared;

namespace Ergenekon.Application.Users.Queries.GetUser;

public record GetUserQuery(string Id) : IRequest<UserSummaryDto?>
{
}

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserSummaryDto?>
{
    private readonly IIdentityService _identityService;

    public GetUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserSummaryDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Id))
            return null;

        return await _identityService.GetUserAsync(request.Id, cancellationToken);
    }
}

