using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllEnabled();
        Task<ProductModel.ProductResponse> Add(ProductModel.ProductRequest request);
        Task<ProductModel.ProductResponse> Update(Guid id, ProductModel.ProductRequest request);
        Task Disable(Guid id);
        Task<ProductModel.ProductResponse> GetById(Guid id);

    }
}
