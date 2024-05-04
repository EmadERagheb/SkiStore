using System.ComponentModel.DataAnnotations;

namespace SkiStore.Domain.DTOs.Basket
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(.1,double.MaxValue,ErrorMessage ="price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="the quantity must be at lest 1 ")]
        public int Quantity { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string Type { get; set; }

    }
}
