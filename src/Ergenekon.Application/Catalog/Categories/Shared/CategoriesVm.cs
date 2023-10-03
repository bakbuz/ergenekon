using Ergenekon.Domain.Entities.Catalog;

namespace Ergenekon.Application.Catalog.Categories.Shared;

public class CategoriesVm
{
    public int Count { get; set; }
    public IList<Category> Categories { get; set; }

}