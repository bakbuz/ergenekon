using Ergenekon.Application.Authentication.Commands.Login;
using Ergenekon.Application.Authentication.Commands.Register;
using Ergenekon.Application.Authentication.Shared;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Application.Authentication.Services;

public interface IAuthenticationService
{
    Task<string> GetUserIdAsync(string email);

    Task<IdentityResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);

    Task<IdentityResult> LoginAsync(LoginCommand command);

    TokenValues CreateToken(string userId);
}
