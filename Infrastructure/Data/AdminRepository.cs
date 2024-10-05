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

        public List<Admin> GetAdminByName(string name)
        {
            return _context.Admins.Where(x => x.Name == name).ToList();
        }
    }
}
