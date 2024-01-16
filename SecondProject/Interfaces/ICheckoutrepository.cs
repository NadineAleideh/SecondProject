using SecondProject.Models;

namespace SecondProject.Interfaces
{
    public interface ICheckoutrepository : IGenericRepository<Checkout>
    {
        Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId);
        Task<List<Checkout>> GetAllWithShippingMethodAsync();
        Task<Checkout> GetWithShippingMethodAsync(int id);
    }
}
