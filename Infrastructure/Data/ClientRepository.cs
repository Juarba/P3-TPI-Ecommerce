﻿using Application.Models.Responses;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly ApplicationContext _context;
        public ClientRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }


        public Client? GetClientByName(string name)
        {
            return _context.Clients.FirstOrDefault(x => x.Name == name);
        }

        public List<Client> GetAll()
        {
            return _context.Clients
                .Include(c => c.SaleOrders) 
                .ToList();
        }
    }
}
