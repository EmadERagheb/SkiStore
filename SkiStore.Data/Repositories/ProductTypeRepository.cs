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
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(SkiStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
