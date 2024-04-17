using AutoMapper;
using SkiStore.API.DTOs.Product;
using SkiStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.API.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetProductDTO>()
                 .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(q => q.ProductType, o => o.MapFrom(s => s.ProductType.Name));
        }
    }
}
