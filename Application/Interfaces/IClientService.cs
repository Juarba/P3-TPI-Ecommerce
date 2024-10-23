using Application.Models;
using Application.Models.Responses;
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
        List<ClientDetailResponseDTO> GetAll();
        Client? Get(int id);
        Client? GetByName(string name);
        void Add(ClientCreateDto clientDto);
        void Update(int id, ClientUpdateDto dto);
        void Delete(int id);

    }
}
