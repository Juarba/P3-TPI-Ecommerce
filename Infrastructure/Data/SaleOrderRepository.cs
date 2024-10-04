﻿using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SaleOrderRepository : BaseRepository<SaleOrder>, ISaleOrderRepository
    {
        private readonly ApplicationContext _context;
        public SaleOrderRepository(ApplicationContext context) : base(context)
        {
            _context = context;

        }
    }
}