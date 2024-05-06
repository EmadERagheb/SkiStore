using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.Models.OrderAggregate
{
    public class OrderItem:BaseDomainModel
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            this.itemOrdered = itemOrdered;
            this.price = price;
            this.quantity = quantity;
        }

        public ProductItemOrdered itemOrdered {  get; set; }

        public decimal price { get; set; }  

        public int quantity { get; set; }
    }
}
