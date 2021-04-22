using Microsoft.EntityFrameworkCore;
using Promo.Core.Interfaces;
using Promo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promo.Core.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PromoActivation> PromoActivations { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasData(
               new Service { Id = 1, Name = "Appvision.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 2, Name = "Analytics.com", Description = "My service description", PromoCode = "itpromocodes" },
               new Service { Id = 3, Name = "Siteconstructor.io", Description = "My service description", PromoCode = "itpromocodes" }
            );
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "User1", Password = "password", FirstName = "John", LastName = "Smith" });
        }
    }

}
