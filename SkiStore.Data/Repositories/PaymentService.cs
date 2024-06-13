using Microsoft.Extensions.Configuration;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.DTOs.Product;
using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;
using Stripe;
using Product = SkiStore.Domain.Models.Product;

namespace SkiStore.Data.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IConfiguration configuration, IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket> CreateOrUpdatePayment(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId is not null)
            {
                var delevieryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync<DeliveryMethodDTO>(d => d.Id == basket.DeliveryMethodId);
                shippingPrice = delevieryMethod.Price;
            }
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync<GetProductDTO>(p => p.Id == item.Id);
                item.Price = product.Price;

            }
            //Strip 
            var serviecs = new PaymentIntentService();
            //Create intent
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }

                };
                var intent = await serviecs.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,

                };
                serviecs.Update(basket.PaymentIntentId, options);
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;

        }

        public async Task<Order> UpdateOrderPaymentFielded(string paymentIntent)
        {
            var order = await _unitOfWork.Repository<Order>().GetAsync<Order>(o => o.PaymentIntentId == paymentIntent);
            if (order is null) return null;
            order.Status = OrderStatus.PaymentFailed.ToString();
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAysnc();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntent)
        {
            var order = await _unitOfWork.Repository<Order>().GetAsync<Order>(o => o.PaymentIntentId == paymentIntent);
            if (order is null) return null;
            order.Status = OrderStatus.PaymentRecevied.ToString();
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAysnc();
            return order;
        }
    }
}
