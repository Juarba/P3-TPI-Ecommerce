using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Total { get; set; }
        public bool Shipment { get; set; }
        public string PaymentMethod { get; set; }
        public Client client { get; set; }
        public List<SaleOrderDetail> SaleOrderDetails { get; set; }


    }
}
