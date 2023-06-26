using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record LoginRequest(string Email, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.Password).NotEmpty().MinimumLength(IdentityConsts.PasswordMinimumLength);
    }
}
