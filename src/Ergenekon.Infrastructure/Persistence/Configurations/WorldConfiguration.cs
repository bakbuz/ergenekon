using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries", "Territory");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.EnglishName).HasMaxLength(100).IsRequired().IsUnicode(false);
        builder.Property(e => e.Iso2Code).HasMaxLength(2).IsUnicode(false);
        builder.Property(e => e.Iso3Code).HasMaxLength(3).IsUnicode(false);
    }
}

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provinces", "Territory");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Abbreviation).HasMaxLength(100);

        builder.HasOne(state => state.Country)
            .WithMany(country => country.Provinces)
            .HasForeignKey(state => state.CountryId)
            .IsRequired();
    }
}

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable("Districts", "Territory");
        builder.HasKey(e => e.Id);

        //builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasOne(d => d.Province)
               .WithMany(p => p.Districts)
               .HasForeignKey(d => d.ProvinceId);
    }
}

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.ToTable("Neighborhoods", "Territory");
        builder.HasKey(e => e.Id);

        //builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasOne(d => d.District)
               .WithMany(p => p.Neighborhoods)
               .HasForeignKey(d => d.DistrictId);
    }
}