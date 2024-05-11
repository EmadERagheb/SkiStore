using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.Models.OrderAggregate;
using System.Collections.Generic;

namespace SkiStore.Domain.Contracts
{
    public interface IOrderService
    {
        Task<GetOrderDTo> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress);
        Task<List<GetOrderDTo>> GetOrdersForUserAsync(string email);
        Task<GetOrderDTo> GetOrderByIdAsync(int id, string email);
        Task<List<DeliveryMethodDTO>> GetDeliveryMethodsAsync();

    }
}
