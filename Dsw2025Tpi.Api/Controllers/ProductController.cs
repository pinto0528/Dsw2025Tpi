using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Application.Dtos;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAll<Product>();

            if (products == null || !products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel.ProductRequest request)
        {
            if (request == null)
            {
                return BadRequest("Product cannot be null");
            }
            var response = await _productService.Add(request);
            return Ok(response);
        }

    }
}
