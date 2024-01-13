using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;
using System.Collections.Generic;

namespace SecondProject.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerrepository
    {
        public CustomerRepository(AppDBContext appDBContext) : base(appDBContext)
        {
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<Customer> GetAsync(Object id)
        {
            return await DbSet.FindAsync(id);
        }

        public override async Task<Customer> AddEntity(Customer entity)
        {
            await DbSet.AddAsync(entity);
            return entity;

        }
        public override async Task<Customer> UpdateEntity(Customer entity, object id)
        {
            var existdata = await DbSet.FindAsync(id);

            existdata.Name = entity.Name;
            existdata.Phone = entity.Phone;
            existdata.Email = entity.Email;
            existdata.CreditLimit = entity.CreditLimit;

            return existdata;



        }

        public override async Task DeleteEntity(object id)
        {
            var existdata = await DbSet.FindAsync(id);
            if (existdata != null)
                DbSet.Remove(existdata);



        }

    }
}
