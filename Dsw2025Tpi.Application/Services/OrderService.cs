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
        private readonly IEntityMapper<Order, OrderModel.OrderResponse> _entityMapper;
        public OrderService(IRepository orderRepository, IEntityMapper<Order, OrderModel.OrderResponse> entityMapper)
        {
            _orderRepository = orderRepository;
            _entityMapper = entityMapper;
        }

        public async Task<OrderModel.OrderResponse> Add(OrderModel.OrderRequest request)
        {
            //Crear diccionario para guardar productos
            var productIds = request.OrderItems.Select(i => i.ProductId).Distinct().ToList();
            var productDict = new Dictionary<Guid, Product>();

            //Cargar diccionario
            foreach (var productId in productIds)
            {
                var product = await _orderRepository.GetById<Product>(productId)
                    ?? throw new InvalidOperationException($"El producto con ID {productId} no fue encontrado.");

                productDict[productId] = product;
            }

            //Validar stock
            foreach (var item in request.OrderItems)
            {
                var product = productDict[item.ProductId];

                if (item.Quantity <= 0)
                {
                    throw new InvalidOperationException($"La cantidad del producto {product.Name} debe ser mayor a cero.");
                }

                if (item.Quantity > product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        $"Stock insuficiente para el producto {product.Name}. " +
                        $"Requerido: {item.Quantity}, Disponible: {product.StockQuantity}"
                    );
                }
            }

            //Procesar la orden
            var orderEntity = request.ToEntity();

            foreach (var item in orderEntity.OrderItems)
            {
                var product = productDict[item.ProductId];

                product.StockQuantity -= item.Quantity;
                item.UnitPrice = product.CurrentUnitPrice;
                item.CalculateSubtotal();
            }

            orderEntity.CalculateTotalAmount();

            var savedOrderEntity = await _orderRepository.Add(orderEntity);
            return _entityMapper.ToResponse(savedOrderEntity);
        }
    }
}