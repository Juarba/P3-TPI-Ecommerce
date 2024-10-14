using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        List<Admin> GetAll();
        Admin? Get(string name);
        Admin? Get(int id);
        void Add(AdminCreateDto adminCreate);
        void Delete(int id);
        void Update(int id, AdminUpdateDto adminUpdate);

    }
}
