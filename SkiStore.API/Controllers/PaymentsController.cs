using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using SkiStore.Domain.Models.OrderAggregate;
using Stripe;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        //Development
        private const string _whSecret = "whsec_85c03b0c6f761d8ed02895c13c4c4916a136f67ac6ad4955c3c5d0f4ed39ddf0";
        //production
        //private const string _whSecret = "whsec_EIJKabWxSGNIjdj3VUH5zPXRlV0zrw5V";


        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePayment(basketId);
            return basket is null ? BadRequest(new APIResponse(400, "problem with your basket")) : Ok(basket);
        }
        [HttpPost("webHook")]
        public async Task<ActionResult> StripWebHooks()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);                                                          //Stripe - Signature"
            PaymentIntent intent;
            Order order;
            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation(message: "payment succeeded: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order update to payment received: ", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogError(message: "payment failed: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFielded(intent.Id);
                    _logger.LogError("Order update to payment failed: ", order.Id);
                    break;

            }
            return new EmptyResult();

        }
    }
}
