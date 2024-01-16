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

        //public Task<List<Customer>> GetAllAsync()
        //{
        //    return base.GetAllAsync();
        //}

        //public async Task<Customer> GetAsync(Object id)
        //{
        //    return await base.GetAsync(id);
        //}

        //public async Task<Customer> AddEntity(Customer entity)
        //{
        //    var customer = await base.AddEntity(entity);

        //    return customer;

        //}
        //public async Task<Customer> UpdateEntity(Customer entity, object id)
        //{
        //    var customer = await base.UpdateEntity(entity, id);

        //    return customer;

        //}

        //public async Task DeleteEntity(object id)
        //{
        //    await base.DeleteEntity(id);
        //}

    }
}
