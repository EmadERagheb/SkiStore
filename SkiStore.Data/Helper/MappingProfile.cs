
using AutoMapper;
using SkiStore.Domain.DTOs.Address;
using SkiStore.Domain.DTOs.Basket;
using SkiStore.Domain.DTOs.Brand;
using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.DTOs.Product;
using SkiStore.Domain.DTOs.ProductType;
using SkiStore.Domain.Identity;
using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;

namespace SkiStore.Data.Helper
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {

            CreateMap<Product, GetProductDTO>()
                 .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(q => q.ProductType, o => o.MapFrom(s => s.ProductType.Name));
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<PostProductDTO, Product>().ReverseMap();
            #region Brand DTOs
            CreateMap<Brand, GetBrandDTO>();
            #endregion
            #region ProductType DTOs
            CreateMap<ProductType, GetProductTypeDTO>();
            #endregion
            #region AddressDTO
            CreateMap<Address, AddressDTO>().ReverseMap();
            #endregion
            #region Basket DTOs
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            #endregion
            #region OrderDTO
            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<Order,Order>().ReverseMap();
            CreateMap<Order, GetOrderDTo>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(a => a.ProductItemId, p => p.MapFrom(p => p.itemOrdered.ProductItemId))
                .ForMember(a => a.ProductName, p => p.MapFrom(p => p.itemOrdered.ProductName))
                .ForMember(a => a.PictureUrl, p => p.MapFrom(p => p.itemOrdered.PictureUrl));
             
              
            CreateMap<ShippingAddress, ShippingAddressDTO>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethod>();    
            CreateMap<DeliveryMethod,DeliveryMethodDTO>();
            #endregion
        }
    }
}
