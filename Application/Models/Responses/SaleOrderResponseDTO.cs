using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class SaleOrderResponseDTO
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string? PaymentMethod { get; set; }
        public ClientResponseDTO? Client { get; set; }
        public List<SaleOrderDetailResponseDTO>? SaleOrderDetails { get; set; } 
    }
}
