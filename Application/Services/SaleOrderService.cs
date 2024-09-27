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
        private readonly ISaleOrderRepository _saleOrderRepository;

        public SaleOrderService(ISaleOrderRepository saleOrderRepository) 
        {
            _saleOrderRepository = saleOrderRepository;
        }

        public List<SaleOrder> GetAll()
        {
            return _saleOrderRepository.Get();
        }

        public SaleOrder AddSaleOrder(SaleOrder order) 
        {
            return _saleOrderRepository.Add(order);
        }

    }
}
