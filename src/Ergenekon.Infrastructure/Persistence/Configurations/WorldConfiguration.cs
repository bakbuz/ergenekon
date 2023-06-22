using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ergenekon.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.EnglishName).HasMaxLength(100).IsRequired().IsUnicode(false);
        builder.Property(e => e.Iso2Code).HasMaxLength(2).IsUnicode(false);
        builder.Property(e => e.Iso3Code).HasMaxLength(3).IsUnicode(false);
    }
}

public class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
{
    public void Configure(EntityTypeBuilder<StateProvince> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Abbreviation).HasMaxLength(100);

        builder.HasOne(state => state.Country)
            .WithMany(country => country.StateProvinces)
            .HasForeignKey(state => state.CountryId)
            .IsRequired();
    }
}

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
    }
}