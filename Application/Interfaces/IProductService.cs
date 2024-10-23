
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService 
    {
        List<Product> GetAll();
        Product? Get(int id);
        
        void Add(ProductCreateDto productDto);
        void Update(int id, ProductUpdateDto productDto);
        void Delete(int id);

        StockStatus CheckStock(int id);
    }
}
