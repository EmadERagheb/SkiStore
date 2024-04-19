using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiStore.Data.DTOs.Product;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;

            _configuration = configuration;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetProducts()
        {

            var products = (await _productRepository.GetAllAsync<GetProductDTO>());
            products.ForEach(q => q.PictureUrl = _configuration["APIURL"] + q.PictureUrl);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync<GetProductDTO>(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }
            product.PictureUrl = _configuration["APIURL"] + product.PictureUrl;
            return product;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(PostProductDTO product)

        {
            try
            {
                var newProduct = await _productRepository.AddAsync<PostProductDTO, ProductDTO>(product);
                newProduct.PictureUrl = _configuration["APIURL"] + product.PictureUrl;

                return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                if (await _productRepository.UpdateAsync(id, product) == 0)
                    return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {


            var product = await _productRepository.GetAsync<Product>(q => q.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productRepository.Exists(q => q.Id == id);
        }
    }
}
