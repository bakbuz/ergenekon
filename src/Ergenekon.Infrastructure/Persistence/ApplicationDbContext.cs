using Duende.IdentityServer.EntityFramework.Options;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Entities.Media;
using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Ergenekon.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }


    public DbSet<Country> Countries { get; set; }
    public DbSet<StateProvince> StateProvinces { get; set; }
    public DbSet<District> Districts { get; set; }

    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Video> Videos { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        // Identity Map
        /*
        const string IdentitySchema = "Identity";
        builder.Entity<ApplicationUser>(b => { b.ToTable("Users", IdentitySchema); });
        builder.Entity<IdentityRole>(b => { b.ToTable("Roles", IdentitySchema); });
        builder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("UserClaims", IdentitySchema); });
        builder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("UserLogins", IdentitySchema).HasKey(e => e.Id); });
        builder.Entity<IdentityUserToken<int>>(b => { b.ToTable("UserTokens", IdentitySchema); });
        builder.Entity<IdentityUserRole<int>>(b => { b.ToTable("UserRoles", IdentitySchema); });
        builder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("RoleClaims", IdentitySchema); });
        */

        /* 
        builder.Entity<TodoList>(b =>
        {
            b.ToTable(nameof(TodoLists));
            b.HasKey(t => t.Id);

            b.Property(e => e.Title).IsRequired().HasMaxLength(100);
            b.OwnsOne(e => e.Colour, navigationBuilder =>
            {
                navigationBuilder.Property(color => color.Code).HasColumnName("ColourCode");
            });

        });

        builder.Entity<TodoItem>(b =>
        {
            b.ToTable(nameof(TodoItems));
            b.HasKey(t => t.Id);

            b.Property(e => e.Title).IsRequired().HasMaxLength(100);
        });
        */
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
