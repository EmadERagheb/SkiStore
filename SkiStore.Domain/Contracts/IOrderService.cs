using SkiStore.Domain.Models.OrderAggregate;

namespace SkiStore.Domain.Contracts
{
    public interface IOrderService 
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress);
    }
}
