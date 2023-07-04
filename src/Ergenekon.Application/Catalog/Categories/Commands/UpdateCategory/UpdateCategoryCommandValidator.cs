using FluentValidation;

namespace Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
    }
}
