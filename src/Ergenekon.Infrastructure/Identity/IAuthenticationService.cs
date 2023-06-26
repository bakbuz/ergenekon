using Ergenekon.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Identity;

public interface IAuthenticationService
{
    TokenValues CreateToken(string userId);

    Task<(IdentityResult, string)> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    Task<(IdentityResult, string)> LoginAsync(LoginRequest request);

    Task<IdentityResult> PasswordRecoveryAsync(PasswordRecoveryRequest request, CancellationToken cancellationToken);

    Task<IdentityResult> PasswordResetAsync(PasswordResetRequest request);

    Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailRequest request);

    Task<IdentityResult> ConfirmEmailChangeAsync(ConfirmEmailChangeRequest request);
}
