using SkiStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.DTOs.Basket
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]

        public List<BasketItemDTO> Items { get; set; }

        public int? DeliveryMethodId { get; set; }

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
