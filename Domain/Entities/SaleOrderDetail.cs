﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
    }
}
