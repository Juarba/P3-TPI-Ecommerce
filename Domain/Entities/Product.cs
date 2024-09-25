using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Product
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public Admin admin { get; set; }
    }
}
