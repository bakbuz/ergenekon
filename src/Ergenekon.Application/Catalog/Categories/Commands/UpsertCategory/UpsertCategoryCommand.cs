using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using MediatR;

namespace Ergenekon.Application.Catalog.Categories.Commands.UpsertCategory;

public record UpsertCategoryCommand(int? Id, string Name, string Description, byte[] Picture) : IRequest<int>
{
}

public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpsertCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entity = null;

        if (request.Id.HasValue)
        {
            entity = await _context.Categories.FindAsync(request.Id.Value);
        }

        if (entity == null)
        {
            entity = new Category();

            _context.Categories.Add(entity);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Picture = request.Picture;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
