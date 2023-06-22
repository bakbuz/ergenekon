using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Country> Countries { get; }
    DbSet<StateProvince> StateProvinces { get; }
    DbSet<District> Districts { get; }

    DbSet<Picture> Pictures { get; }
    DbSet<Video> Videos { get; }

    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
