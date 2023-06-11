using CustomerManagement.Data.Models;
using CustomerManagement.Data.UOW;
using CustomerManagement.WebApi.BusinessLogic.Contracts;

namespace CustomerManagement.WebApi.BusinessLogic.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _unitOfWork.Customer.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(long id, bool includeChildren = false)
    {
        return await _unitOfWork.Customer.GetByIdAsync(id, includeChildren);
    }

    public async Task AddCustomerAsync(Customer Customer)
    {
        await _unitOfWork.Customer.InsertAsync(Customer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(Customer updatedCustomer)
    {
        _unitOfWork.Customer.UpdateAsync(updatedCustomer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(Customer Customer)
    {
        _unitOfWork.Customer.DeleteAsync(Customer);
        await _unitOfWork.SaveChangesAsync();
    }
}