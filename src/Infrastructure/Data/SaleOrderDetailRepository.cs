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
    public class SaleOrderDetailRepository : BaseRepository<SaleOrderDetail>, ISaleOrderDetailRepository
    {
        private readonly ApplicationContext _context;

        public SaleOrderDetailRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public List<SaleOrderDetail> GetAllBySaleOrder(int saleOrderId)
        {
            return _context.SaleOrderDetails
                .Include(p => p.Product)
                .Include(s => s.SaleOrder)
                .ThenInclude(c => c.Client)
                .Where(x => x.Id == saleOrderId)
                .ToList();
        }

        public List<SaleOrderDetail> GetAllByProduct(int productId)
        {
            return _context.SaleOrderDetails
                .Include(p => p.Product)
                .Include(s => s.SaleOrder)
                .ThenInclude(c => c.Client)
                .Where(x => x.Product.Id == productId)
                .ToList();
        }

        public SaleOrderDetail? Get(int id)
        {
            return _context.SaleOrderDetails
                .Include(p => p.Product)
                .FirstOrDefault(x => x.Id == id);
        }
    }

}

   

