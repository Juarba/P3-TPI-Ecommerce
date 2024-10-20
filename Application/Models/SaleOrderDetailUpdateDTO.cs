using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SaleOrderDetailUpdateDTO
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
