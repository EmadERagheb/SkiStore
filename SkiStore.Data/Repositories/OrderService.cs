using AutoMapper;
using Microsoft.Extensions.Configuration;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.DTOs.Product;
using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;
using System.Collections.Generic;

namespace SkiStore.Data.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository,IConfiguration configuration,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GetOrderDTo> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress)
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
                await _basketRepository.DeleteBasketAsync(basketId);
                newOrder.DeliveryMethod = deliveryMethod;
                var orderDTO = _mapper.Map<GetOrderDTo>(newOrder);
                return orderDTO;

            }
            else return null;
            //return order
        }

        public async Task<List<GetOrderDTo>> GetOrdersForUserAsync(string email)
        {
           var orders=     await _unitOfWork.Repository<Order>().GetAllAsync<GetOrderDTo>(1, 10
                , c => c.BuyerEmail == email
                , o => o.CreatedDate
                , null, nameof(Order.DeliveryMethod));
            if (orders != null)
            {
                orders.ForEach(o => o.OrderItems.ForEach(p => p.PictureUrl = _configuration["APIURL"] + p.PictureUrl));
            }
            return orders;
        }

        public async Task<GetOrderDTo>GetOrderByIdAsync(int id,string email)
        {
        var  order= await _unitOfWork.Repository<Order>()
                .GetAsync<GetOrderDTo>(o => o.Id == id && o.BuyerEmail == email,
                nameof(Order.DeliveryMethod));
            if (order is not null) 
                order.OrderItems.ForEach(p => p.PictureUrl = _configuration["APIURL"] + p.PictureUrl);
            return order;
        }

        public async Task<List<DeliveryMethodDTO>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync<DeliveryMethodDTO>(1, 10);
        }
    }
}
