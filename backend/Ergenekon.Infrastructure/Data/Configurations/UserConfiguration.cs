using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.HasKey(u => new { u.FollowerId, u.FollowingId });
    }
}
