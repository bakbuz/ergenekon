using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergenekon.Application.Catalog.Products.Services;

public interface IProductService : ICrudService<Product>
{
}
