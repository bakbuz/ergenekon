using Ergenekon.Domain.Entities.Listings;

namespace Ergenekon.Application.Catalog.Categories.Shared;

public class CategoriesVm
{
    public int Count { get; set; }
    public IList<Category> Categories { get; set; }

}