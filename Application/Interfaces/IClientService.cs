using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        List<Client> GetAll();
        Client? Get(int id);
        List<Client> GetByName(string name);
        void Add(ClientCreateDto clientDto);
        void Update(int id);
        void Delete(int id);

    }
}
