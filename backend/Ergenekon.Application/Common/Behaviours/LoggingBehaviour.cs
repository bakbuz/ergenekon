using Ergenekon.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Ergenekon.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUser currentUser, IIdentityService identityService)
    {
        _logger = logger;
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string? requestName = typeof(TRequest).Name;
        string? userName = string.Empty;

        if (_currentUser.Id.HasValue)
        {
            userName = await _identityService.GetUsernameAsync(_currentUser.Id.Value, cancellationToken);
        }

        _logger.LogInformation("Ergenekon Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, _currentUser.Id, userName, request);
    }
}
