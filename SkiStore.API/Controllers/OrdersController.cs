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
        private readonly IOrderService _orderService;
        private readonly IAuthManger _authManger;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderRepository, IAuthManger authManger, IMapper mapper)
        {
            _orderService = orderRepository;
            _authManger = authManger;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<GetOrderDTo>> CreateOrder(OrderDTO orderDTO)
        {


            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _authManger.GetCurrentUserAsync(email);
            ShippingAddress shippingAAddress = _mapper.Map<ShippingAddress>(orderDTO.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, shippingAAddress);
            if (order is null)
                return BadRequest(new APIResponse(400, "problem creating order"));
            else
                return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<List<GetOrderDTo>>> GetOrders()
        {
            var email = User.Claims.FirstOrDefault(c=>c.Type== ClaimTypes.Email)?.Value;
            List<GetOrderDTo> orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(orders);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTo>> GetOrder(int id)
        {
            var email = User.Claims.FirstOrDefault(c=>c.Type== ClaimTypes.Email)?.Value;
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order is null)
                return NotFound(new APIResponse(404));
            
            return Ok(order);
        }
        [HttpGet("deliveryMethods")]
        public async Task<List<DeliveryMethodDTO>> GetDeliveryMethods()
        {
           return await _orderService.GetDeliveryMethodsAsync();
        }



    }
}
