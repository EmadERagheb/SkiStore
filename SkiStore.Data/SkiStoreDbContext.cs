using Microsoft.EntityFrameworkCore;

namespace SkiStore.Data
{
    public class SkiStoreDbContext:DbContext
    {
        public SkiStoreDbContext(DbContextOptions<SkiStoreDbContext> options):base(options)
        {
            
        }
    }
}
