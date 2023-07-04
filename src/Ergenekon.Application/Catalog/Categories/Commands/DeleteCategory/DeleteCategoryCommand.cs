using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest
{
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _categoryService.GetByIdAsync(request.Id);
        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        await _categoryService.DeleteAsync(entity, cancellationToken);
    }
}