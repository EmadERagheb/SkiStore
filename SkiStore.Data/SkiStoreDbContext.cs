using Microsoft.EntityFrameworkCore;
using SkiStore.Domain.Models;

namespace SkiStore.Data
{
    public class SkiStoreDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public SkiStoreDbContext(DbContextOptions<SkiStoreDbContext> options):base(options)
        {
            
        }
    }
}
