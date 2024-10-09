using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public void Add(ClientCreateDto clientDto)
        {
            var client = new Client()
            {
                Name = clientDto.Name,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Password = clientDto.Password,
                UserRol = clientDto.UserRole,
            };

            _repository.Add(client);
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
