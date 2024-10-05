using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAll()
        {
            return _repository.Get();
        }

        public Product? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Product> GetByName(string name)
        {
            return _repository.GetProductByName(name);
        }
        public void Add(int id)
        {
            var productAdd = _repository.Get(id);
            if (productAdd != null)
            {
                _repository.Add(productAdd);
            }    
        }

        public void Update(int id)
        {
            var productUpdate = _repository.Get(id);
            if (productUpdate != null)
            {
                _repository.Update(productUpdate);
            }
        }

        public void Delete(int id)
        {
            var productDelete = _repository.Get(id);
            if (productDelete != null)
            {
                _repository.Delete(productDelete);
            }
        }
    }
}
