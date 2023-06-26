using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record RegisterRequest(string Username, string Email, string Password);

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(m => m.Username).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.Password).NotEmpty().MinimumLength(IdentityConsts.PasswordMinimumLength);
    }
}
