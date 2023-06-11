using System;
using CustomerManagement.Data.Models;
using CustomerManagement.Data.Repository.Contract;

namespace CustomerManagement.Data.Repository.Service
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ApplicationDbContext context) : base(context) { }
    }
}