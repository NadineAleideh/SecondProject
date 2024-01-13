using Microsoft.EntityFrameworkCore;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Repository;

namespace SecondProject.UoW
{
    public class UnitOfwork : IUnitofWork
    {

        public CustomerRepository Customerrepository { get; private set; }

        private bool _disposed = false;
        public DbContext Context => _dbContext;

        //DB context injection 
        private readonly AppDBContext _dbContext;

        public UnitOfwork(AppDBContext appDBContext)
        {
            this._dbContext = appDBContext;
            Customerrepository = new CustomerRepository(_dbContext);//right or not?
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        //ensures proper resource cleanup
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
