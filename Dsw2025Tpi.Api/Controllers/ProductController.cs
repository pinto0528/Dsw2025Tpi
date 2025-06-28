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
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllEnabled();

            if (products == null || !products.Any())
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel.ProductRequest request)
        {

            try
            {
                var response = await _productService.Add(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException knte)
            {
                return BadRequest(knte.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductModel.ProductRequest request)
        {
            try
            {
                var response = await _productService.Update(id, request);
                return Ok(response);
            }


            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException knte)
            {
                return BadRequest(knte.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPatch("{id:guid}")]

        public async Task<IActionResult> Disable([FromRoute] Guid id)
        {
            try
            {
                await _productService.Disable(id);
                return NoContent();
            }
            catch (KeyNotFoundException knte)
            {
                return NotFound(knte.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById ([FromRoute] Guid id)
        {
            try
            {
                var response = await _productService.GetById(id);
                return Ok(response);
            }
            catch (KeyNotFoundException knte)
            {
                return NotFound();
            }


            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
