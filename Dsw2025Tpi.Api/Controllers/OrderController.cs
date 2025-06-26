using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IService _orderService;
        public OrderController(IService orderService)
        {
            _orderService = orderService;
        }



    }
}