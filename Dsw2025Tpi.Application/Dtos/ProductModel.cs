using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Dtos
{
    public record ProductModel
    {
        public record ProductRequest(
            string Sku,
            string InternalCode,
            string Name,
            string Description,
            decimal CurrentUnitPrice,
            int StockQuantity

            ) : IRequestMapper<Product>
        {
            public Product ToEntity() 
            {
                return new Product
                {
                    Name = Name,
                    Sku = Sku,
                    InternalCode = InternalCode,
                    Description = Description,
                    CurrentUnitPrice = CurrentUnitPrice,
                    StockQuantity = StockQuantity,
                    IsActive = true // Valor por defecto para nuevos productos

                };
            }
        };

        public record ProductResponse(
            Guid Id, 
            string Name,
            string Sku,
            string InternalCode,
            string Description,
            decimal CurrentUnitPrice,
            int StockQuantity,
            bool IsActive
            );
    }
}