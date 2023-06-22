using Duende.IdentityServer.EntityFramework.Options;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
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

public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

/* builder.Entity<TodoList>(b =>
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
        });*/
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
