using System;
using Moq;
using CustomerManagement.Data.Models;
using CustomerManagement.WebApi.BusinessLogic.Contracts;
using Microsoft.Extensions.Logging;
using CustomerManagement.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.UnitTests.Controllers;

public class CustomersControllerTests
{
    public CustomersControllerTests()
    {
    }

    [Fact]
    public async Task GetCustomers_ReturnsOkResultWithCustomers()
    {
        // Arrange
        var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Wilson", Email = "John.Wilson@yopmail.com" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Wilson", Email = "Jane.Wilson@yopmail.com" },
            };

        var mockService = new Mock<ICustomerService>();
        mockService.Setup(service => service.GetAllCustomersAsync()).ReturnsAsync(customers);

        var mockLogger = new Mock<ILogger<CustomersController>>();

        var controller = new CustomersController(mockService.Object, mockLogger.Object);

        // Act
        var result = await controller.GetCustomers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCustomers = Assert.IsType<List<Customer>>(okResult.Value);
        Assert.Equal(customers.Count, returnedCustomers.Count);
    }

}

