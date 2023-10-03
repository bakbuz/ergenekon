using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities.Catalog;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;

public record AddCategoryImageCommand(int Id, string Picture) : IRequest;

public class AddCategoryImageCommandHandler : IRequestHandler<AddCategoryImageCommand>
{
    private readonly ICategoryService _categoryService;

    public AddCategoryImageCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task Handle(AddCategoryImageCommand request, CancellationToken cancellationToken)
    {
        Category? entity = await _categoryService.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.Picture = request.Picture;

        await _categoryService.UpdateAsync(entity, cancellationToken);
    }
}
