using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public sealed record RegisterRequest(string Username, string Email, string Password);

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(m => m.Username).NotEmpty().WithName("Kullanıcı adı");
        RuleFor(m => m.Email).NotEmpty().WithName("E-posta").EmailAddress();
        RuleFor(m => m.Password).NotEmpty().WithName("Parola").MinimumLength(IdentityConsts.PasswordMinimumLength);
    }
}
