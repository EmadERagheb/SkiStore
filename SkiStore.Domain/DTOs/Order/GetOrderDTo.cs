using SkiStore.Domain.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.DTOs.Order
{
    public class GetOrderDTo
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTime OrderDate { get; set; } 



        public ShippingAddressDTO ShipToAddress { get; set; }

    
        public string DeliveryMethod { get; set; }
        public string ShippingPrice { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }

        public decimal Subtotal { get; set; }
        [EnumDataType(typeof(OrderStatus))]
        public string Status { get; set; } 

        public decimal Total { get; set; }

     
    }
}
