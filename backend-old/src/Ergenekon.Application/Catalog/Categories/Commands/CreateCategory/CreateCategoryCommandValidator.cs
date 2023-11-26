using FluentValidation;

namespace Ergenekon.Application.Catalog.Categories.Commands.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
    }
}
