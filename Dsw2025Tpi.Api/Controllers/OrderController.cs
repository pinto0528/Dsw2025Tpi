using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Interfaces;


namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel.Request request)
        {
            try
            {
                var response = await _orderService.Add(request);
                return Ok(response);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado." });
            }

        }
    }
}