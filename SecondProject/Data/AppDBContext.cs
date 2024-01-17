using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondProject.Models;
using System.Threading.Tasks;

namespace SecondProject.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
