using SkiStore.Domain.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.DTOs.Order
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }

        public ShippingAddressDTO ShippingAddress { get; set; }

    }
}
