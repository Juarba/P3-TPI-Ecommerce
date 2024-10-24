﻿using Domain.Entities;
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
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
