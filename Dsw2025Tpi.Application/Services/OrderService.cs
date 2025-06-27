using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Interfaces;

namespace Dsw2025Tpi.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _orderRepository;
        private readonly IEntityMapper<Order, OrderModel.Response> _entityMapper;
        public OrderService(IRepository orderRepository, IEntityMapper<Order, OrderModel.Response> entityMapper)
        {
            _orderRepository = orderRepository;
            _entityMapper = entityMapper;
        }

        public async Task<OrderModel.Response> Add(OrderModel.Request request)
        {

            var orderEntity = request.ToEntity();
            foreach (var item in orderEntity.OrderItems)
            {
                var product = await _orderRepository.GetById<Product>(item.ProductId) ??
                    throw new InvalidOperationException($"El producto con ID {item.ProductId} no fue encontrado.");

                if (item.Quantity > product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        $"Stock insuficiente para el producto {product.Name}. " +
                        $"Requerido: {item.Quantity}, " +
                        $"Disponible: {product.StockQuantity}"
                    );
                }

                product.StockQuantity -= item.Quantity;
                product.CurrentUnitPrice = item.UnitPrice;

                await _orderRepository.Update(product);
            }

            var savedOrderEntity = await _orderRepository.Add(orderEntity);

            return _entityMapper.ToResponse(savedOrderEntity);
        }
    }
}