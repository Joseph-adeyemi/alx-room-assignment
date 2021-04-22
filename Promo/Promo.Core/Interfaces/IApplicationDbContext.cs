using Microsoft.EntityFrameworkCore;
using Promo.Core.Models;
using System.Threading.Tasks;

namespace Promo.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<PromoActivation> PromoActivations { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChanges();
    }
}