using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SaleOrderRepository : BaseRepository<SaleOrder>, ISaleOrderRepository
    {
        private readonly ApplicationContext _context;
        public SaleOrderRepository(ApplicationContext context) : base(context)
        {
            _context = context;

        }

        public List<SaleOrder> GetAllByClient(int clientId)
        {
            return _context.SaleOrders
                .Include(cl => cl.Client)
                .Include(od => od.SaleOrderDetails)
                .ThenInclude(p => p.Product)
                .Where(x => x.ClientId == clientId)
                .ToList();
        }

        public SaleOrder? Get(int id)
        {
            return _context.SaleOrders
                .Include(cl => cl.Client)
                .Include(s => s.SaleOrderDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(x => x.Id == id);

        }
    }
}
