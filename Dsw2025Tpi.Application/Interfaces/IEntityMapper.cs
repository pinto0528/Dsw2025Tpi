using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Interfaces
{
    public interface IEntityMapper<TEntity, TResponse>
    {
        TResponse ToResponse(TEntity entity);
    }
}