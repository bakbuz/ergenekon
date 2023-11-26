using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Persistence.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.HasKey(page => page.Id);

        builder.Property(page => page.Slug).IsUnicode(false).HasMaxLength(200).IsRequired();
        builder.Property(page => page.Title).IsUnicode(true).HasMaxLength(200).IsRequired();
        builder.Property(page => page.Code).IsUnicode(true).HasMaxLength(50);
    }
}

