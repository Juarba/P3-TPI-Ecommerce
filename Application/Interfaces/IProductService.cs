
using Application.Models;
using Domain.Entities;
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
        
        List<Product> GetByName(string name);
        void Add(ProductCreateDto productDto);
        void Update(int id);
        void Delete(int id);
        
    }
}
