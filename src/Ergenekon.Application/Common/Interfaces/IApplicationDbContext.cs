using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Country> Countries { get; }
    DbSet<Province> Provinces { get; }
    DbSet<District> Districts { get; }
    DbSet<Neighborhood> Neighborhoods { get; }

    DbSet<Picture> Pictures { get; }
    DbSet<Video> Videos { get; }

    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<Page> Pages { get; }
    DbSet<SearchTerm> SearchTerms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
