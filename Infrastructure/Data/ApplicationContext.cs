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
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }



        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserRol);

            modelBuilder.Entity<Client>()
                .HasMany(s => s.SaleOrders)
                .WithOne(c => c.Client);

            modelBuilder.Entity<SaleOrder>()
                .HasMany(o => o.SaleOrderDetails)
                .WithOne(s => s.SaleOrder);

            modelBuilder.Entity<SaleOrderDetail>()
                .HasOne(p => p.Product)
                .WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }


}