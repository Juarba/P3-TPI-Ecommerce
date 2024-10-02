using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<Client> Clients { get; set; }



        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }


}