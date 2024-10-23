﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Client? GetClientByName(string name);
        List<Client> GetAll();
    }
}
