using Microsoft.EntityFrameworkCore;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Repository;
using SecondProject.strategy;

namespace SecondProject.UoW
{
    public class UnitOfwork : IUnitofWork
    {

        public ICustomerrepository _Customerrepository { get; private set; }
        public ICheckoutrepository _Checkoutrepository { get; private set; }
        public IProductRepository _Productrepository { get; private set; }


        public DbContext Context => _dbContext;

        //DB context injection 
        private readonly AppDBContext _dbContext;

        public UnitOfwork(AppDBContext appDBContext)
        {
            this._dbContext = appDBContext;
            _Customerrepository = new CustomerRepository(_dbContext);
            _Checkoutrepository = new CheckoutRepository(_dbContext);
            _Productrepository =new ProductRepository(_dbContext);

        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        //ensures proper resource cleanup
        public void Dispose()
        {
            _dbContext.Dispose();


        }


    }
}
