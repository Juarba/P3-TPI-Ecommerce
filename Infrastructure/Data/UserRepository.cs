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
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
           
        }

        public List<User> GetUserByName(string name)
        {
            return _context.Users.Where(x => x.Name == name).ToList();
        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }

    }
}
