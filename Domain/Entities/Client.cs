using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User
    {
        public string product { get; set; }
        public int purchase { get; set; }
        public Client()
        {
            UserRol = Rol.Client;
        }
    }
}
