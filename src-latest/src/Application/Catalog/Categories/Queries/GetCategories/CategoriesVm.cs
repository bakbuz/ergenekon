using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.Catalog.Categories.Queries.GetCategories;

public class CategoriesVm
{
    public int Count { get; set; }
    public IList<Category> Categories { get; set; }

}