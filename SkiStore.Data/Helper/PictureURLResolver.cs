using AutoMapper;
using Microsoft.Extensions.Configuration;
using SkiStore.Data.DTOs.Product;
using SkiStore.Domain.Models;


namespace SkiStore.Data.Helper
{
    public class PictureURLResolver : IValueResolver<Product, GetProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public PictureURLResolver(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(Product source, GetProductDTO destination, string destMember, ResolutionContext context)
        {
          
            return _configuration["APIURL"] + source.PictureUrl;
        }
    }
}
