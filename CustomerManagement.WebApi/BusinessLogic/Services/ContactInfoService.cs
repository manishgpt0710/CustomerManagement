using CustomerManagement.Data.Models;
using CustomerManagement.Data.UOW;
using CustomerManagement.WebApi.BusinessLogic.Contracts;

namespace CustomerManagement.WebApi.BusinessLogic.Services;

public class ContactInfoService : IContactInfoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactInfoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ContactInfo>> GetAllAsync(long parentId)
    {
        return await _unitOfWork.ContactInfo.FindAsync(c => c.CustomerId == parentId);
    }

    public async Task<ContactInfo?> GetByIdAsync(long id)
    {
        return await _unitOfWork.ContactInfo.GetByIdAsync(id);
    }

    public async Task AddAsync(ContactInfo contactInfo)
    {
        await _unitOfWork.ContactInfo.InsertAsync(contactInfo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<ContactInfo> contactInfos)
    {
        await _unitOfWork.ContactInfo.InsertRangeAsync(contactInfos);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(ContactInfo contactInfo)
    {
        _unitOfWork.ContactInfo.UpdateAsync(contactInfo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(ContactInfo contactInfo)
    {
        _unitOfWork.ContactInfo.DeleteAsync(contactInfo);
        await _unitOfWork.SaveChangesAsync();
    }

}