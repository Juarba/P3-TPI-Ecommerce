using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SaleOrderCreateDTO
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Shipment { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public Client client { get; set; }
    }
}
