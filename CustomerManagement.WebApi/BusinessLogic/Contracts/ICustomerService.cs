using System;
using CustomerManagement.Data.Models;

namespace CustomerManagement.WebApi.BusinessLogic.Contracts;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(long id, bool includeChildren = false);
    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Customer customer);
}
