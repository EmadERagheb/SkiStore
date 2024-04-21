using AutoMapper;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(SkiStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
