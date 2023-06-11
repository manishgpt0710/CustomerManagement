using System;
using CustomerManagement.Data.Models;
using CustomerManagement.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data.Repository.Service
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly DbSet<Customer> entities;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            entities = context.Set<Customer>();
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await entities.AsNoTracking().Include(c => c.ContactInfos).Include(c => c.Orders).ToListAsync();
        }

        public override async Task<Customer?> GetByIdAsync(long id, bool includeChildren = false)
        {
            if (includeChildren)
            {
                return await entities.AsNoTracking().Where(a => a.Id == id)?.Include(c => c.ContactInfos)?.Include(c => c.Orders).FirstOrDefaultAsync();
            }
            return await entities.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
        }
    }
}