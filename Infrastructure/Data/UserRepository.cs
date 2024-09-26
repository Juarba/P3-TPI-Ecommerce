using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(ApplicationContext context) : base(context)
        {
           
        }

        public List<User> GetUserByName(string name)
        {
            return _context.Users.Where(p => p.Name == name).ToList();
        }
  
    }
}
