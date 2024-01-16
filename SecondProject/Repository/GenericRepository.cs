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
        public virtual async Task<T> AddEntity(T entity)
        {
            await this.DbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteEntity(Object id)
        {
            var entityToDelete = await DbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                DbSet.Remove(entityToDelete);
            }
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return this.DbSet.ToListAsync();
        }

        public virtual async Task<T> GetAsync(Object id)
        {

            return await this.DbSet.FindAsync(id);
        }

        public virtual async Task<T> UpdateEntity(T entity, object id)
        {
            var existingEntity = await DbSet.FindAsync(id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            return existingEntity;
        }
    }
}
