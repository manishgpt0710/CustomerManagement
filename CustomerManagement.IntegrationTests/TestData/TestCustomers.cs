using System;
using CustomerManagement.Data.Models;

namespace CustomerManagement.IntegrationTests.TestData
{
    public class TestCustomers
    {
        public static Customer CusotmerWithOutDetails { get; } = new Customer
        {
            FirstName = "Test",
            LastName = "CustomerWOD",
            Email = "Test.CustomerWOD@yopmail.com",
            Created = new DateTime(2023, 6, 11, 1, 23, 24, DateTimeKind.Utc)
        };

        public static Customer CusotmerWithContactInfo { get; } = new Customer
        {
            FirstName = "Test",
            LastName = "CustomerWCI",
            Email = "Test.CustomerWCI@yopmail.com",
            Created = new DateTime(2023, 6, 11, 1, 23, 24, DateTimeKind.Utc),
            ContactInfos = new List<ContactInfo>()
            {
                new ContactInfo
                {
                    Phone = "12313627",
                    Address1 = "Test Address1",
                    City = "Test City",
                    State = "Test State",
                    Country = "Test Country",
                    ZipCode = "Test ZipCode"
                }
            }
        };
    }
}

