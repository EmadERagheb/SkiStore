using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiStore.API.Errors;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.DTOs.Basket;
using SkiStore.Domain.Models;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? Ok(new CustomerBasket(id)) : Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO customerBasketDTO)
        {
            var customerBasket = await _basketRepository.GetBasketAsync(customerBasketDTO.Id);
            if (customerBasket is null)
            {
                return NotFound(new APIResponse(404));
            }
            _mapper.Map(customerBasketDTO, customerBasket);
            var updateBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            return updateBasket is null ? NotFound() : Ok(updateBasket);
        }
        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
