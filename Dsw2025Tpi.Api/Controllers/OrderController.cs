using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IProductService _orderService;
        public OrderController(IProductService orderService)
        {
            _orderService = orderService;
        }



    }
}