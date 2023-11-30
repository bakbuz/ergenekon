using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record LoginRequest(string Email, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(m => m.Email).NotEmpty().WithName("E-posta").EmailAddress();
        RuleFor(m => m.Password).NotEmpty().WithName("Parola").MinimumLength(IdentityConstants.PasswordMinimumLength);
    }
}
