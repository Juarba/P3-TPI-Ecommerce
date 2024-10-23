using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public List<Admin> GetAll()
        {
            return _repository.Get();
        }

        public Admin? Get(string name)
        {
            return _repository.Get(name);
        }
        public Admin? Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(AdminCreateDto adminDto)
        {
            var admin = new Admin()
            {
                Name = adminDto.Name,
                LastName = adminDto.LastName,
                Email = adminDto.Email,
                Password = adminDto.Password,
                UserRol = "Admin"
            };
            _repository.Add(admin);
        }

        public void Delete(int id)
        {
            var adminToDelete = _repository.Get(id);
            if (adminToDelete is not null)
            {
                _repository.Delete(adminToDelete);
            }
        }

        public void Update(int id, AdminUpdateDto update)
        {
            var adminToUpdate = _repository.Get(id);

            if (adminToUpdate is null)
                throw new NotFoundException();

            if (adminToUpdate is not null)
            {
                adminToUpdate.Email = update.Email;
                adminToUpdate.Password = update.Password;

                _repository.Update(adminToUpdate);
            }
        }
    }
}