using Ergenekon.Application.Authentication.Services;
using Ergenekon.Application.Authentication.Shared;
using Ergenekon.Application.Common.Models;
using MediatR;

namespace Ergenekon.Application.Authentication.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<(Result Result, TokenValues? TokenValues)>
{

}
public class LoginCommandHandler : IRequestHandler<LoginCommand, (Result Result, TokenValues? TokenValues)>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<(Result Result, TokenValues? TokenValues)> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.LoginAsync(command);
        if (!result.Succeeded)
            return (Result.Failure(result.Errors.Select(s => s.Description).AsEnumerable()), null);

        var userId = await _authenticationService.GetUserIdAsync(command.Email);
        var tokenValues = _authenticationService.CreateToken(userId);
        return (Result.Success(), tokenValues);
    }
}