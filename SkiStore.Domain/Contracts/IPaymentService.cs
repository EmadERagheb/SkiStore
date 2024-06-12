using SkiStore.Domain.Models;
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
    }
}
