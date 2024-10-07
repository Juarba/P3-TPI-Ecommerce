using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleOrderService
    {
        List<SaleOrder> GetAll();
        SaleOrder? Get(int id);
        void Add(int id);
        void Update(int id);
        void Delete(int id);

    }
}
