using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.Contracts
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePayment(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntent);
        Task<Order> UpdateOrderPaymentFielded(string paymentIntent);
    }
}
