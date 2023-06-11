using System;
using CustomerManagement.Data.Repository.Contract;
using CustomerManagement.Data.Repository.Service;

namespace CustomerManagement.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IContactInfoRepository ContactInfo
        {
            get;
            private set;
        }
        public IOrderRepository Order
        {
            get;
            private set;
        }
        public ICustomerRepository Customer
        {
            get;
            private set;
        }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            ContactInfo = new ContactInfoRepository(this.context);
            Order = new OrderRepository(this.context);
            Customer = new CustomerRepository(this.context);
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
