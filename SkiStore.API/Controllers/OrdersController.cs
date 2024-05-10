using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Order;
using SkiStore.Domain.Models.OrderAggregate;
using System.Security.Claims;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderRepository;
        private readonly IAuthManger _authManger;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderRepository, IAuthManger authManger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _authManger = authManger;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {

        
           var email= User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Email)?.Value;
            var user = await _authManger.GetCurrentUserAsync(email);
            ShippingAddress shippingAAddress = _mapper.Map<ShippingAddress>(orderDTO.ShippingAddress);
            Order order = await _orderRepository.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, shippingAAddress);
            if (order is null)
                return BadRequest(new APIResponse(400, "problem creating order"));
            else
                return Ok(order);

        }
    }
}
