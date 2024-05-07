using Microsoft.EntityFrameworkCore;
using SkiStore.Domain.Models;
using System.Reflection;

namespace SkiStore.Data
{
    public class SkiStoreDbContext : DbContext
    {
        TimeZoneInfo timeZoneInfo { get; set; } = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public SkiStoreDbContext(DbContextOptions<SkiStoreDbContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(500);
            configurationBuilder.Properties<decimal>().HavePrecision(16, 2);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace == "SkiStore.Data.Configurations.SkiStore");
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseDomainModel>().Where(q =>
              q.State == EntityState.Added || q.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
