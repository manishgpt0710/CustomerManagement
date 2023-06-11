using System;
using CustomerManagement.Data.Repository.Contract;

namespace CustomerManagement.Data.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customer
        {
            get;
        }
        IContactInfoRepository ContactInfo
        {
            get;
        }
        IOrderRepository Order
        {
            get;
        }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
