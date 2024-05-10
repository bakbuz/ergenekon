using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Constants;
using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ergenekon.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<UserFollow> UserFollows { get; set; }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        IdentityModelCreating0(builder);
    }

    private void IdentityModelCreating0(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable(nameof(Users), Identities.IdentitySchema);
        builder.Entity<ApplicationRole>().ToTable(nameof(Roles), Identities.IdentitySchema);
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable(nameof(RoleClaims), Identities.IdentitySchema);
        builder.Entity<IdentityUserRole<Guid>>().ToTable(nameof(UserRoles), Identities.IdentitySchema);
        builder.Entity<IdentityUserClaim<Guid>>().ToTable(nameof(UserClaims), Identities.IdentitySchema);
        builder.Entity<IdentityUserLogin<Guid>>().ToTable(nameof(UserLogins), Identities.IdentitySchema);
        builder.Entity<IdentityUserToken<Guid>>().ToTable(nameof(UserTokens), Identities.IdentitySchema);
    }
}
