using SecondProject.Repository;
using SecondProject.strategy;

namespace SecondProject.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        ICustomerrepository _Customerrepository { get; }
        ICheckoutrepository _Checkoutrepository { get; }
        IProductRepository _Productrepository { get; }
        Task SaveChangesAsync();
    }
}
