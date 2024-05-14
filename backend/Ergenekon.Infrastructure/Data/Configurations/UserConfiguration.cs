using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Data.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired();

        //builder.HasOne<ApplicationUser>().WithOne();
    }
}

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.HasKey(u => new { u.FollowerId, u.FollowingId });
    }
}
