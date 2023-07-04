using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;

public record RemoveCategoryImageCommand(int Id) : IRequest;

public class RemoveCategoryImageCommandHandler : IRequestHandler<RemoveCategoryImageCommand>
{
    private readonly ICategoryService _categoryService;

    public RemoveCategoryImageCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task Handle(RemoveCategoryImageCommand request, CancellationToken cancellationToken)
    {
        Category? entity = await _categoryService.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.Picture = null;

        await _categoryService.UpdateAsync(entity, cancellationToken);
    }
}
