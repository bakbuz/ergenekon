using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities.Listings;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(int ParentId, string Name, string Description) : IRequest<int>
{
    internal string? Picture { get; set; }

    public CreateCategoryCommand SetPicture(string? picture)
    {
        Picture = picture;
        return this;
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = new Category();
        entity.ParentId = request.ParentId;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Picture = request.Picture;

        await _categoryService.CreateAsync(entity, cancellationToken);

        return entity.Id;
    }
}
