using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleOrderDetailService
    {
        List<SaleOrderDetail> GetAllBySaleOrder(int saleOrderId);
        List<SaleOrderDetail> GetAllByProducts(int productId);
        SaleOrderDetail? Get(int id);
        void Add(SaleOrderDetailCreateDTO dto);
        void Delete(int id);
        void Update(int id, SaleOrderDetailUpdateDTO dto);


    }
}
