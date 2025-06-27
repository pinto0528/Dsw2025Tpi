using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Application.Dtos;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAll<Product>();
            return products.Any() ? Ok(products) : NoContent();
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel.ProductRequest request)
        {

            var response = await _productService.Add(request);
            return Ok(response);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductModel.ProductRequest request)
        {
            var response = await _productService.Update(id, request);
            return Ok(response);
        }

        [HttpPatch("{id:guid}")]

        public async Task<IActionResult> Disable([FromRoute] Guid id)
        {

            await _productService.Disable(id);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById ([FromRoute] Guid id)
        {
            
            var response = await _productService.GetById(id);
            return Ok(response);
            
        }
    }
}
