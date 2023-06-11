using System;
using CustomerManagement.Data.Models;
using CustomerManagement.Data.Repository.Contract;

namespace CustomerManagement.Data.Repository.Service
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}