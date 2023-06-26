using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record ConfirmEmailChangeRequest(string UserId, string Email, string Code);

internal class ConfirmEmailChangeValidator : AbstractValidator<ConfirmEmailChangeRequest>
{
    public ConfirmEmailChangeValidator()
    {
        RuleFor(m => m.UserId).NotEmpty();
        RuleFor(m => m.Code).NotEmpty();
        RuleFor(m => m.Email).NotEmpty().EmailAddress();
    }
}
