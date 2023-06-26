using Ergenekon.Application.Authentication.Services;
using Ergenekon.Application.Authentication.Shared;
using Ergenekon.Application.Common.Models;
using MediatR;

namespace Ergenekon.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(string Username, string Email, string Password) : IRequest<(Result Result, TokenValues? TokenValues)>
{

}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (Result Result, TokenValues? TokenValues)>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<(Result Result, TokenValues? TokenValues)> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RegisterAsync(command, cancellationToken);
        if (!result.Succeeded)
            return (Result.Failure(result.Errors.Select(s => s.Description).AsEnumerable()), null);

        var userId = await _authenticationService.GetUserIdAsync(command.Email);
        var tokenValues = _authenticationService.CreateToken(userId);
        return (Result.Success(), tokenValues);
    }
}