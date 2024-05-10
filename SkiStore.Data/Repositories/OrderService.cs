using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.DTOs.Product;
using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;

namespace SkiStore.Data.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress)
        {
            //get basket from the repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //get items from  the product repo
            var orderItems = new List<OrderItem>();

            foreach (BasketItem item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync<ProductDTO>(q => q.Id == item.Id);
                var productOrderd = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productOrderd, product.Price, item.Quantity);
                orderItems.Add(orderItem);
                //_unitOfWork.Repository<OrderItem>().Add(orderItem);
            }
            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync<DeliveryMethod>(q => q.Id == deliveryMethodId);

            //clac subtotal
            var subtotal = orderItems.Sum(q => q.quantity * q.price);


            //create order 
            Order newOrder = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethodId, subtotal);
            _unitOfWork.Repository<Order>().Add(newOrder);
            //save to db
            if (await _unitOfWork.CompleteAysnc() > 0)
            {
              await  _basketRepository.DeleteBasketAsync(basketId);
                newOrder.DeliveryMethod=deliveryMethod;
                return newOrder;

            }
            else return null;
            //return order
        }
    }
}
