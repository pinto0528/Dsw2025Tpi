﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Interfaces
{
    public interface IRequestMapper<TEntity>
    {
        TEntity ToEntity();
    }
}
