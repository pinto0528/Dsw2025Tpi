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
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel.OrderRequest request)
        {
            var response = await _orderService.Add(request);
            return Ok("Orden creada exitosamente.\n" + response);  
        }
    }
}