using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Basket;
using SkiStore.Domain.Models;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>>CreateOrUpdatePaymentIntent(string basketId)
        {
               var basket= await _paymentService.CreateOrUpdatePayment(basketId) ;
            return basket is null ? NotFound(new APIResponse(404)) : Ok(basket);
        }
    }
}
