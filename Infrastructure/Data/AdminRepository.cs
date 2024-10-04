using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Admin? GetAdminByName(string name)
        {
            return _context.Admins.FirstOrDefault(x => x.Name==name);
        }

        public Admin Add(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return admin;
        }

        public void Delete(Admin admin) 
        {
        _context.Remove(admin);
        _context.SaveChanges();
        }

        public List<Admin> GetAll() 
        { 
            return _context.Admins.ToList();
        }
        public void Update(Admin admin)
        {
            _context.Update(admin);
            _context.SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }   
}
