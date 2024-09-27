using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SaleOrderRepository : BaseRepository<SaleOrder>, ISaleOrderRepository
    {
        public SaleOrderRepository(ApplicationContext context) : base(context)
        {

        }

        public List<SaleOrder> GetSaleOrderRepositoryById(int id) 
        {
            return _context.SaleOrders.Where(x=>x.OrderId == id).ToList();
        }
    }
}
