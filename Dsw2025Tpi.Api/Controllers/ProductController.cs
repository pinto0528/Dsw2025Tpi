using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IService _productService;
        public ProductController(IService productService)
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

    }
}
