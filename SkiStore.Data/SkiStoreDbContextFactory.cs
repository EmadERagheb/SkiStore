using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data
{
    public class SkiStoreDbContextFactory : IDesignTimeDbContextFactory<SkiStoreDbContext>
    {
        private readonly IConfiguration _configuration;

        public SkiStoreDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SkiStoreDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<SkiStoreDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new SkiStoreDbContext(optionBuilder.Options);
        }
    }
}
