
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SkiStore.Data.DTOs.Product;
using SkiStore.Domain.Models;

namespace SkiStore.Data.Helper
{
    public class MappingProfile : Profile
    {
       

        public MappingProfile()
        {

            CreateMap<Product, GetProductDTO>()
                 .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(q => q.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                 .ForMember(q => q.PictureUrl, o => o.MapFrom<PictureURLResolver>());
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<PostProductDTO, Product>().ReverseMap();
        }
    }
}
