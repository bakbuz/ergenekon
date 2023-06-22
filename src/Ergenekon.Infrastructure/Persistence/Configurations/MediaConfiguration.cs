using Ergenekon.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Persistence.Configurations;

public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.RelativePath).HasMaxLength(400).IsRequired().IsUnicode(false);
    }
}

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.VideoUrl).HasMaxLength(400).IsRequired().IsUnicode(false);
    }
}
