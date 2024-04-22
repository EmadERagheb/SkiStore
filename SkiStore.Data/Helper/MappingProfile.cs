
using AutoMapper;
using SkiStore.Data.DTOs.Brand;
using SkiStore.Data.DTOs.Product;
using SkiStore.Data.DTOs.ProductType;
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


            #region Brand DTOs
            CreateMap<Brand, GetBrandDTO>();
            #endregion
            #region ProductType DTOs
            CreateMap<ProductType, GetProductTypeDTO>();
            #endregion
        }
    }
}
