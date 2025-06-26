using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Interfaces
{
    public interface IService
    {
        Task<IEnumerable<T>?> GetAll<T>() where T : EntityBase;
    }
}
