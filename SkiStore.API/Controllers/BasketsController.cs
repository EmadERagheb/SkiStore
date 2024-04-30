using Microsoft.AspNetCore.Mvc;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? Ok(new CustomerBasket(id)) : Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updateBasket = await _basketRepository.UpdateBasketAsync(basket);
            return updateBasket is null ? NotFound() : Ok(updateBasket);
        }
        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
         { 
        return  await  _basketRepository.DeleteBasketAsync(id);
        }
    }
}
