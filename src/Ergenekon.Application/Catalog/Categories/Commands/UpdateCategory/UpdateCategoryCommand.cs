using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int ParentId, string Name, string Description, byte[]? Picture) : IRequest
{
    internal int Id { get; set; }

    public UpdateCategoryCommand SetId(int id)
    {
        Id = id;
        return this;
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryService _categoryService;

    public UpdateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entity = await _categoryService.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Category), request.Id);

        entity.ParentId = request.ParentId;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Picture = request.Picture;

        await _categoryService.UpdateAsync(entity, cancellationToken);
    }
}
