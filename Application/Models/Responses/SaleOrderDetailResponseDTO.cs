using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class SaleOrderDetailResponseDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int SaleOrderId { get; set; }
        public ProductResponseDTO? Product { get; set; }
    }
}
