using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;
using System.Collections.Generic;

namespace SecondProject.Repository
{
    public class CheckoutRepository : GenericRepository<Checkout>, ICheckoutrepository
    {
        public CheckoutRepository(AppDBContext appDBContext) : base(appDBContext)
        {
        }



        public async Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId)
        {

            return await DbSet
                .Include(c => c.shippingMethod)
                .Where(c => c.ShippingMethodId == shippingMethodId)
                .Select(c => c.shippingMethod)
                .FirstOrDefaultAsync();
        }


        public async Task<List<Checkout>> GetAllWithShippingMethodAsync()
        {
            return await this.DbSet.Include(c => c.shippingMethod).ToListAsync();
        }

        public async Task<Checkout> GetWithShippingMethodAsync(int id)
        {
            return await this.DbSet
                .Include(c => c.shippingMethod)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
