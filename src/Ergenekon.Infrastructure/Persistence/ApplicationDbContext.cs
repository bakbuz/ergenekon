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
        //builder.Entity<ApplicationUser>(b => { b.ToTable("Users", IdentityConsts.IdentitySchema); });
        //builder.Entity<IdentityRole>(b => { b.ToTable("Roles", IdentityConsts.IdentitySchema); });
        //builder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("UserClaims", IdentityConsts.IdentitySchema); });
        //builder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("UserLogins", IdentityConsts.IdentitySchema).HasKey(e => e.Id); });
        //builder.Entity<IdentityUserToken<int>>(b => { b.ToTable("UserTokens", IdentityConsts.IdentitySchema); });
        //builder.Entity<IdentityUserRole<int>>(b => { b.ToTable("UserRoles", IdentityConsts.IdentitySchema); });
        //builder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("RoleClaims", IdentityConsts.IdentitySchema); });

        //_operationalStoreOptions.DefaultSchema = IdentityConsts.IdentitySchema;

        //builder.Entity(nameof(UserClaims)).ToTable(nameof(UserClaims), IdentityConsts.IdentitySchema);
        //builder.Entity(nameof(UserLogins)).ToTable(nameof(UserLogins), IdentityConsts.IdentitySchema);
        //builder.Entity(nameof(UserTokens)).ToTable(nameof(UserTokens), IdentityConsts.IdentitySchema);
        //builder.Entity(nameof(UserRoles)).ToTable(nameof(UserRoles), IdentityConsts.IdentitySchema);
        //builder.Entity(nameof(RoleClaims)).ToTable(nameof(RoleClaims), IdentityConsts.IdentitySchema);
        

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
