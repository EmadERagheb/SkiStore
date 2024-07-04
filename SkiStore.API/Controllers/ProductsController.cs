using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiStore.Domain.DTOs.Product;
using SkiStore.Data.Helper;
using SkiStore.Data.Specifications;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using System.Linq.Expressions;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;

        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;

            _configuration = configuration;
        }

        // GET: api/Products
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagging<GetProductDTO>>> GetProducts([FromQuery] ProductSpecPrams productPrams)
        {

            Expression<Func<Product, object>> sortAsc = default;
            Expression<Func<Product, object>> sortDesc = default;
            Expression<Func<Product, bool>> filter = p =>
            (string.IsNullOrEmpty(productPrams.Search) || p.Name.ToLower().Contains(productPrams.Search.ToLower())) &&
            (!productPrams.BrandId.HasValue || p.BrandId == productPrams.BrandId) &&  //
            (!productPrams.ProductTypeId.HasValue || p.ProductTypeId == productPrams.ProductTypeId);

            switch (productPrams.Sort)
            {
                case "priceAsc":
                    sortAsc = p => p.Price;
                    break;
                case "priceDesc":
                    sortDesc = p => p.Price;
                    break;
                default:
                    sortAsc = p=>p.Name;
                    break;
            }



            List<GetProductDTO> products = await _productRepository.GetAllAsync<GetProductDTO>(productPrams.PageIndex, productPrams.PageSize, filter, sortAsc, sortDesc);
            products.ForEach(q => q.PictureUrl = _configuration["APIURL"]  +'/' + q.PictureUrl);
            var productCount = await _productRepository.GetCountAsync(filter);
            return Ok(new Pagging<GetProductDTO>()
            {
                Count = productCount,
                Data = products,
                PageIndex = productPrams.PageIndex,
                PageSize = productPrams.PageSize
            });
        }

        // GET: api/Products/5
        [Cached(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync<GetProductDTO>(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }
            product.PictureUrl = _configuration["APIURL"] +"/"+ product.PictureUrl;
            return product;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region Updated later
        //[HttpPost]
        //public async Task<ActionResult<ProductDTO>> PostProduct(PostProductDTO product)

        //{
        //    try
        //    {
        //        var newProduct = await _productRepository.AddAsync<PostProductDTO, ProductDTO>(product);
        //        newProduct.PictureUrl = _configuration["APIURL"] + product.PictureUrl;

        //        return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //} 
        #endregion

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region Update Put Later
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        if (await _productRepository.UpdateAsync(id, product) == 0)
        //            return NotFound();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //    return NoContent();
        //}

        #endregion
        // DELETE: api/Products/5
        #region UPDATE DELETE LATER
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{

        //    var result = await _productRepository.DeleteAsync(id);

        //    if (result == 0)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        #endregion
        private async Task<bool> ProductExists(int id)
        {
            return await _productRepository.Exists(q => q.Id == id);
        }
    }
}
