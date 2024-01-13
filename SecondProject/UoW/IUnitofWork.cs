using SecondProject.Repository;

namespace SecondProject.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        CustomerRepository Customerrepository { get; }

        Task SaveChangesAsync();
    }
}
