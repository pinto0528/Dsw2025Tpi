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
    public class ProductMapper : IEntityMapper<Product, ProductModel.Response>
    {
        public ProductModel.Response ToResponse(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Product entity cannot be null");
            }
            return new ProductModel.Response(
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
