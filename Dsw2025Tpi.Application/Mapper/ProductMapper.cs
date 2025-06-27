using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Mapper
{
    public class ProductMapper : IEntityMapper<Product, ProductModel.ProductResponse>
    {
        public ProductModel.ProductResponse ToResponse(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Product entity cannot be null");
            }
            return new ProductModel.ProductResponse(
                entity.Id,
                entity.Name,
                entity.Sku,
                entity.InternalCode,
                entity.Description,
                entity.CurrentUnitPrice,
                entity.StockQuantity,
                entity.IsActive
            );
        }
    }
}
