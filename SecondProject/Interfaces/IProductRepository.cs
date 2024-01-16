using SecondProject.Models;

namespace SecondProject.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByNameAsync(string productName);
    }
}
