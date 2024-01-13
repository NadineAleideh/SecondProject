using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecondProject.Data;
using SecondProject.Interfaces;
using System.Collections.Generic;

namespace SecondProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //The following Variable is going to hold the DbSet Entity
        public DbSet<T> DbSet { get; set; }

        //DB context injection 
        private readonly AppDBContext _dbContext;

        public GenericRepository(AppDBContext appDBContext)
        {
            this._dbContext = appDBContext;
            this.DbSet = this._dbContext.Set<T>();
        }
        public virtual Task<T> AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteEntity(Object id)
        {
            throw new NotImplementedException();
        }

        public  Task<List<T>> GetAllAsync()
        {
            return this.DbSet.ToListAsync();
        }

        public virtual Task<T> GetAsync(Object id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> UpdateEntity(T entity, object id)
        {
            throw new NotImplementedException();
        }
    }
}
