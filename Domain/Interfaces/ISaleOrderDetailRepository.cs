using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleOrderDetailRepository : IBaseRepository<SaleOrderDetail>
    {
        List<SaleOrderDetail> GetAllBySaleOrder(int saleOrderId);
        List<SaleOrderDetail> GetAllByProduct(int productId);
    }
}
