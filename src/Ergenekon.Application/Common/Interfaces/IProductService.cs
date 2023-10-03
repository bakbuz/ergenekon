using Ergenekon.Domain.Entities.Catalog;

namespace Ergenekon.Application.Common.Interfaces;

public interface IProductService : ICrudService<Product, Guid>
{
}
