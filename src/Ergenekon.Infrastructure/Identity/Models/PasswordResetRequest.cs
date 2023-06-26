using Ergenekon.Domain.Consts;
using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record PasswordResetRequest(string Email, string Password, string Code);

internal class PasswordResetRequestValidator : AbstractValidator<PasswordResetRequest>
{
    public PasswordResetRequestValidator()
    {
        RuleFor(m => m.Code).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.Password).NotEmpty()
            .MinimumLength(IdentityConsts.PasswordMinimumLength)
            .MaximumLength(IdentityConsts.PasswordMaximumLength);
    }
}
