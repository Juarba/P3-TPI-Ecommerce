using Application.Interfaces;
using Application.Models;
using Application.Models.Responses;
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

        public List<ClientDetailResponseDTO> GetAll()
        {
            var clients = _repository.GetAll();
            return clients.Select(c => new ClientDetailResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
                LastName = c.LastName,
                Email = c.Email,
                UserRol = c.UserRol,
                SaleOrderCount = c.SaleOrders.Count 
            }).ToList();
        }

        public Client? Get(int id)
        {
            return _repository.Get(id);
        }

        public Client? GetByName(string name) 
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
                UserRol = "Client",
            };

            _repository.Add(client);
        }

        public void Update(int id, ClientUpdateDto dto)
        {
            var clientUpdate = _repository.Get(id);
            if (clientUpdate != null)
            {
                clientUpdate.Name = dto.Name;
                clientUpdate.LastName = dto.LastName;
                clientUpdate.Email = dto.Email;

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
