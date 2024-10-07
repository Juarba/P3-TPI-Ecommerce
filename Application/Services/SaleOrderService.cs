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
    public class SaleOrderService:ISaleOrderService
    {
        private readonly ISaleOrderRepository _repository;

        public SaleOrderService(ISaleOrderRepository repository) 
        {
            _repository = repository;
        }

        public List<SaleOrder> GetAll()
        {
            return _repository.Get();
        }

        public SaleOrder? Get(int id) 
        { 
        return _repository.Get(id);
        }

        public void Add(int id) 
        {
            var product =_repository.Get(id);
            if (product is not null) 
            {
                _repository.Add(product);
            }
        }

        public void Update(int id) 
        {
            var product = _repository.Get(id);

            if (product is not null) 
            {
                _repository.Update(product);
            }
        }

        public void Delete(int id) 
        {
            var product = _repository.Get(id);
            if(product is not null) 
            {
                _repository.Delete(product);
            }
        }


    }
}
