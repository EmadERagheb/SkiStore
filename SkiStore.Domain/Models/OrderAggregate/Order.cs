namespace SkiStore.Domain.Models.OrderAggregate
{
    public class Order : BaseDomainModel
    {
        public Order()
        {
            
        }

        public Order(List<OrderItem> orderItems, string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,  decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;



        public Address ShipToAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal * DeliveryMethod.Price;
        }


    }
}
