using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Application.Dtos;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _productRepository;
        private readonly IEntityMapper<Product, ProductModel.ProductResponse> _entityMapper;
        public ProductService(IRepository productRepository, IEntityMapper<Product, ProductModel.ProductResponse> entityMapper)
        {
            _productRepository = productRepository;
            _entityMapper = entityMapper;
        }

        public async Task<IEnumerable<T>?> GetAll<T>() where T : EntityBase
        {
            return await _productRepository.GetAll<T>();
        }

        public async Task<ProductModel.ProductResponse> Add(ProductModel.ProductRequest request) 
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El request no puede ser nulo.");
            }

            // Validacion de campos requeridos
            if (request.CurrentUnitPrice <= 0)
            {
                throw new ArgumentException("El precio unitario debe ser mayor a cero.");
            }

            if(request.StockQuantity < 0)
            {
                throw new ArgumentException("La cantidad de stock no puede ser negativa.");
            }

            var productEntity = request.ToEntity();
            var savedProductEntity = await _productRepository.Add(productEntity); 
            return _entityMapper.ToResponse(savedProductEntity);
        }

        public async Task<ProductModel.ProductResponse> Update(Guid id, ProductModel.ProductRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El request no puede ser nulo.");
            }

            // Validacion de existencia del producto
            var existingProduct = await _productRepository.GetById<Product>(id) ??
                throw new KeyNotFoundException($"El producto de ID {id} no fue encontrado.");

            // Validacion de campos requeridos
            if (request.CurrentUnitPrice <= 0)
            {
                throw new ArgumentException("No se puede actualizar. El precio unitario debe ser mayor a cero.");
            }

            if (request.StockQuantity < 0)
            {
                throw new ArgumentException("No se puede actualizar. La cantidad de stock no puede ser negativa.");
            }

            // Actualizacion de campos
            existingProduct.Sku = request.Sku;
            existingProduct.InternalCode = request.InternalCode;
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.CurrentUnitPrice = request.CurrentUnitPrice;
            existingProduct.StockQuantity = request.StockQuantity;

            // Actualizacion de DB
            var savedProduct = await _productRepository.Update(existingProduct);

            return _entityMapper.ToResponse(savedProduct);
        }
        public async Task Disable(Guid id)
        {
            var product = await _productRepository.GetById<Product>(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"El producto de ID {id} no fue encontrado.");
            }

            product.Disable();
            await _productRepository.Update(product);
        }
    }
}
