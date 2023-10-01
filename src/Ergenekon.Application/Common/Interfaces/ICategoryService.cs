using Ergenekon.Domain.Entities.Listings;

namespace Ergenekon.Application.Common.Interfaces;

public interface ICategoryService : ICrudService<Category, int>
{
    Task<List<Category>> GetCategoriesByParentId(int parentId, CancellationToken cancellationToken);
}
