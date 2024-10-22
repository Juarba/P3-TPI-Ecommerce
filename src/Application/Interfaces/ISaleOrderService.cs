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
    public interface ISaleOrderService
    {
        List<SaleOrderResponseDTO> GetAllByClient(int clientId);
        SaleOrderResponseDTO? Get(int id);
        void Add(SaleOrderCreateDTO createSaleOrder);
        void Update(int id,SaleOrderUpdateDTO update);
        void Delete(int id);

    }
}
