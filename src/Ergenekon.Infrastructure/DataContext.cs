using Ergenekon.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Infrastructure
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<StateProvince> StateProvinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Country & StateProvince Map
            modelBuilder.Entity<Country>(b =>
            {
                b.ToTable("Countries");
                b.HasKey(e => e.Id);
                b.Property(e => e.Name).HasMaxLength(100).IsRequired();
                b.Property(e => e.EnglishName).HasMaxLength(100).IsRequired().IsUnicode(false);
                b.Property(e => e.Iso2Code).HasMaxLength(2).IsUnicode(false);
                b.Property(e => e.Iso3Code).HasMaxLength(3).IsUnicode(false);
            });

            modelBuilder.Entity<StateProvince>(b =>
            {
                b.ToTable("StateProvinces");
                b.HasKey(e => e.Id);
                b.Property(e => e.Name).HasMaxLength(100).IsRequired();
                b.Property(e => e.Abbreviation).HasMaxLength(100);

                b.HasOne(state => state.Country)
                    .WithMany(country => country.StateProvinces)
                    .HasForeignKey(state => state.CountryId)
                    .IsRequired();
            });
        }
    }
}