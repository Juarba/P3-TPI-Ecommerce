using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SaleOrderDetailCreateDTO
    {
        [Required]
        public int SaleOrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
