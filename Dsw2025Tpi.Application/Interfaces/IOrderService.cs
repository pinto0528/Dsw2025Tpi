﻿using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel.Response> Add(OrderModel.Request request);
    }
}
