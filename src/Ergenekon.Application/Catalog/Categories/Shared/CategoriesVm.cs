using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.Catalog.Categories.Shared;

public class CategoriesVm
{
    public int Count { get; set; }
    public IList<Category> Categories { get; set; }

}