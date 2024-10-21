﻿using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product? Get(string name);

        List<Product> GetProductByName(string name);

        StockStatus CheckStock(int productId);
    }
}
