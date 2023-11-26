using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Entities.Catalog;
using Ergenekon.Domain.Entities.Listings;
using Ergenekon.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // To-do lists
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }

    // Territory
    DbSet<Country> Countries { get; }
    DbSet<Province> Provinces { get; }
    DbSet<District> Districts { get; }
    DbSet<Neighborhood> Neighborhoods { get; }

    // Catalog
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    // Listings
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<ListingPicture> ListingPictures { get; set; }

    // Others
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<SearchTerm> SearchTerms { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<RequestLog> RequestLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
