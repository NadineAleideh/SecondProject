using Microsoft.EntityFrameworkCore;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDBContext appDBContext) : base(appDBContext)
        {
        }

        public async Task<Product> GetByNameAsync(string productName)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Name == productName);
        }
    }
}
