using Application.Models;
using Application.Models.Responses;
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
        List<SaleOrderDetailResponseDTO> GetAllBySaleOrder(int saleOrderId);
        List<SaleOrderDetailResponseDTO> GetAllByProducts(int productId);
        SaleOrderDetailResponseDTO? Get(int id);
        void Add(SaleOrderDetailCreateDTO dto);
        void Delete(int id);
        void Update(int id, SaleOrderDetailUpdateDTO dto, int saleOrderId);


    }
}
