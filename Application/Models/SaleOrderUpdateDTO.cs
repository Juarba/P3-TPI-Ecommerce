using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SaleOrderUpdateDTO
    {
        
        public int Price { get; set; } = 0;
        
        public bool? Shipment { get; set; } = null;
        
        public string PaymentMethod { get; set; } = string.Empty;

        public Client? client { get; set; } = null;
    }
}
