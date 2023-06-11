using System;
using CustomerManagement.Data;
using CustomerManagement.Data.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.IntegrationTests.TestData.Factories
{
    public class TestCustomerFactory
    {
        public TestCustomerFactory()
        {
        }

        public static async Task CreateCustomers(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var c1 = TestCustomers.CusotmerWithOutDetails;
            await unitOfWork.Customer.InsertAsync(c1);

            var c2 = TestCustomers.CusotmerWithContactInfo;
            await unitOfWork.Customer.InsertAsync(c2);

            await services.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();
        }
    }
}
