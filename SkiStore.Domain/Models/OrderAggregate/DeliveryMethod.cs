namespace SkiStore.Domain.Models.OrderAggregate
{
    public class DeliveryMethod : BaseDomainModel
    {
        public string ShortName { get; set; }

        public string DeliveryTime { get; set; }

        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
