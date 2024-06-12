using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Data.Repositories
{
    public class PaymentService : IPaymentService
    {
        public Task<CustomerBasket> CreateOrUpdatePayment(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
