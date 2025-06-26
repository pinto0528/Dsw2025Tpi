using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services
{
    public class OrderService : IService
    {
        private readonly IRepository _orderRepository;
        public OrderService(IRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
