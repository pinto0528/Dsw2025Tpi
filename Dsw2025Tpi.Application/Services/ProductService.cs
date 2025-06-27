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
            var productEntity = request.ToEntity();
            var savedProductEntity = await _productRepository.Add(productEntity); 
            return _entityMapper.ToResponse(savedProductEntity);
        }

        public async Task<ProductModel.ProductResponse> Update(Guid id, ProductModel.ProductRequest request)
        {
            // Validacion de existencia del producto
            var existingProduct = await _productRepository.GetById<Product>(id) ??
                throw new KeyNotFoundException($"El producto de ID {id} no fue encontrado.");

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
    }
}
