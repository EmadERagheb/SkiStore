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

        public SkiStoreDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("F:\\MyWork\\API\\SkiStore\\SkiStore.API\\appsettings.json", false, true)
                .Build();
            var optionBuilder = new DbContextOptionsBuilder<SkiStoreDbContext>();
            optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new SkiStoreDbContext(optionBuilder.Options);
        }
    }
}
