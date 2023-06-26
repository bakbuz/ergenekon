using FluentValidation;

namespace Ergenekon.Infrastructure.Identity.Models;

public record ConfirmEmailRequest(string UserId, string Code);

internal class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailValidator()
    {
        RuleFor(m => m.UserId).NotEmpty();
        RuleFor(m => m.Code).NotEmpty();
    }
}
