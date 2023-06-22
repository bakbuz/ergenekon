using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Country> Countries { get; }
    DbSet<StateProvince> StateProvinces { get; }
    DbSet<District> Districts { get; }

    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
