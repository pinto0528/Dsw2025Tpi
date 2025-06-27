using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderModel
    {
        public record OrderRequest(
            Guid CustomerId,
            string ShippingAddress,
            string BillingAddress,
            string Notes,
            List<OrderItemModel.ItemRequest> OrderItems
            ) : IRequestMapper<Order>
        {
            public Order ToEntity()
            {
                return new Order
                {
                    CustomerId = CustomerId,
                    ShippingAddress = ShippingAddress,
                    BillingAddress = BillingAddress,
                    Notes = Notes,
                    OrderItems = OrderItems.Select(item => item.ToEntity()).ToList(),

                };
            }
        };

        public record OrderResponse(
            Guid Id,
            Guid CustomerId,
            string ShippingAddress,
            string BillingAddress,
            string Notes,
            List<OrderItemModel.ItemResponse> OrderItems
            );

    }
}