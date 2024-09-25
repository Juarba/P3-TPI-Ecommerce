using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class SaleOrder
    {
        public int OrderId { get; set; }
        public int Price { get; set; }
        public bool Shipment { get; set; }
        public string PaymentMethod { get; set; }
        public Client client { get; set; }


    }
}
