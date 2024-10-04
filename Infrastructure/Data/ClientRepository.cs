﻿using Domain.Entities;
using Domain.Interfaces;
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

        public List<Client> GetClientByName(string name)
        {
            return _context.Clients.Where(x => x.Name == name).ToList();
        }
    }
}
