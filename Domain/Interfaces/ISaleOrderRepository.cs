using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleOrderRepository : IBaseRepository<SaleOrder>
    {
        List<SaleOrder> GetAllByClient(int clientId);
        SaleOrder? Get(int id);
    }
}
