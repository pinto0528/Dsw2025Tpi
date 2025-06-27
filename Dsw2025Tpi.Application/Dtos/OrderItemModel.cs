using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderItemModel
    {
        public record ItemRequest(
            Guid ProductId,
            int Quantity
            ) : IRequestMapper<OrderItem>
        {
            public OrderItem ToEntity()
            {
                var item = new OrderItem
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    
                };
                return item;
            }
        };

        public record ItemResponse(
            Guid Id,
            Guid ProductId,
            int Quantity,
            decimal UnitPrice,
            decimal Subtotal
            );

    }
}