using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<Client> GetAll()
        {
            return _repository.Get();
        }

        public Client? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Client> GetByName(string name) 
        {
            return _repository.GetClientByName(name);
        }
        public void Add(int id)
        {
            var clientAdd = _repository.Get(id);
            if (clientAdd != null)
            {
                _repository.Add(clientAdd);   
            }
           
        }

        public void Update(int id)
        {
            var clientUpdate = _repository.Get(id);
            if (clientUpdate != null)
            {
                _repository.Update(clientUpdate);
            }
        }

        public void Delete(int id)
        {
            var clientDelete = _repository.Get(id);
            if (clientDelete != null)
            {
                _repository.Delete(clientDelete);
            }

        }




    }
}
