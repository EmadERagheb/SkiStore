using System.ComponentModel.DataAnnotations;

namespace SkiStore.Domain.DTOs.Product
{
    public abstract class BaseProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
     
    }
}
