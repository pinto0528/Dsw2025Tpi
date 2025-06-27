﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Mapper
{
    public class OrderMapper : IEntityMapper<Order, OrderModel.Response>
    {
        public OrderModel.Response ToResponse(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Product entity cannot be null");
            }
            return new OrderModel.Response(
                entity.Id,
                entity.CustomerId,
                entity.ShippingAddress,
                entity.BillingAddress,
                entity.OrderItems.Select(item => new OrderItemModel.ItemResponse(
                    item.Id,
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.Subtotal
                )).ToList()
            );
        }
    }
}