using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Consts;
using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Entities.Media;
using Ergenekon.Infrastructure.Identity;
using Ergenekon.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Ergenekon.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly OperationalStoreOptions _operationalStoreOptions;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _operationalStoreOptions = operationalStoreOptions.Value;
    }

    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }


    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }

    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Video> Videos { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<SearchTerm> SearchTerms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        // Identity Map
        builder.Entity<ApplicationUser>().ToTable("Users", IdentityConsts.IdentitySchema);
        builder.Entity<IdentityRole>().ToTable("Roles", IdentityConsts.IdentitySchema);
        builder.Entity<IdentityUserClaim<string>>().ToTable(nameof(UserClaims), IdentityConsts.IdentitySchema);
        builder.Entity<IdentityUserLogin<string>>().ToTable(nameof(UserLogins), IdentityConsts.IdentitySchema);
        builder.Entity<IdentityUserToken<string>>().ToTable(nameof(UserTokens), IdentityConsts.IdentitySchema);
        builder.Entity<IdentityUserRole<string>>().ToTable(nameof(UserRoles), IdentityConsts.IdentitySchema);
        builder.Entity<IdentityRoleClaim<string>>().ToTable(nameof(RoleClaims), IdentityConsts.IdentitySchema);

        builder.Entity<Key>().ToTable(nameof(Keys), IdentityConsts.IdentityServerSchema);
        builder.Entity<PersistedGrant>().ToTable(nameof(PersistedGrants), IdentityConsts.IdentityServerSchema);
        builder.Entity<DeviceFlowCodes>().ToTable(nameof(DeviceFlowCodes), IdentityConsts.IdentityServerSchema);
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
