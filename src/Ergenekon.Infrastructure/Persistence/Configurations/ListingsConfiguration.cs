using Ergenekon.Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Persistence.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(100).IsRequired();
    }
}

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Slug).HasMaxLength(100).IsUnicode(false);
        builder.Property(e => e.Title).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Summary).HasMaxLength(500);
    }
}

public class ListingPictureConfiguration : IEntityTypeConfiguration<ListingPicture>
{
    public void Configure(EntityTypeBuilder<ListingPicture> builder)
    {
        builder.HasKey(e => e.Id);
    }
}