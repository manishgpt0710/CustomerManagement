using System;
using CustomerManagement.Data.Models;

namespace CustomerManagement.WebApi.BusinessLogic.Contracts;

public interface IContactInfoService
{
    Task<IEnumerable<ContactInfo>> GetAllAsync(long parentId);
    Task<ContactInfo?> GetByIdAsync(long id);
    Task AddAsync(ContactInfo contactInfo);
    Task AddRangeAsync(IEnumerable<ContactInfo> contactInfos);
    Task UpdateAsync(ContactInfo contactInfo);
    Task DeleteAsync(ContactInfo contactInfo);
}
