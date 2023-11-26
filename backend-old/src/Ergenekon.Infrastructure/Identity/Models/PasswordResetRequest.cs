using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record PasswordResetRequest(string Email, string Password, string Code);

public class PasswordResetRequestValidator : AbstractValidator<PasswordResetRequest>
{
    public PasswordResetRequestValidator()
    {
        RuleFor(m => m.Code).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().WithName("E-posta").EmailAddress();
        RuleFor(m => m.Password).NotEmpty().WithName("Parola")
            .MinimumLength(IdentityConsts.PasswordMinimumLength)
            .MaximumLength(IdentityConsts.PasswordMaximumLength);
    }
}
