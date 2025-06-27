using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductService : IService
    {
        private readonly IRepository _productRepository;
        public ProductService(IRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<T>?> GetAll<T>() where T : EntityBase
        {
            return await _productRepository.GetAll<T>();
        }
    }
}
