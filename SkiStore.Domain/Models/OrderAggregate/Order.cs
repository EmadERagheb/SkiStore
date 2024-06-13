using System.ComponentModel.DataAnnotations;

namespace SkiStore.Domain.Models.OrderAggregate
{
    public class Order : BaseDomainModel
    {
        public Order()
        {

        }



        public Order(List<OrderItem> orderItems, string buyerEmail,
            ShippingAddress shipToAddress,
            int deliveryMethodId, 
            decimal subtotal,
            string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            OrderItems = orderItems;
            Subtotal = subtotal;
            DeliveryMethodId = deliveryMethodId;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;



        public ShippingAddress ShipToAddress { get; set; }

        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }
        [EnumDataType(typeof(OrderStatus))]
        public string Status { get; set; } = OrderStatus.Pending.ToString();

        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }


    }
}
