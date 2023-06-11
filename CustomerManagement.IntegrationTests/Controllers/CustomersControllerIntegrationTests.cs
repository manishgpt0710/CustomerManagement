using CustomerManagement.Data.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CustomerManagement.IntegrationTests.Controllers;

[Collection("IntegrationTests")]
public class CustomersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CustomersControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomers_ReturnsOkResult()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/api/customers");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomer_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var existingId = 1;

        // Act
        var response = await _client.GetAsync($"/api/customers/{existingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCustomer_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var nonExistingId = 999;

        // Act
        var response = await _client.GetAsync($"/api/customers/{nonExistingId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_ValidData_ReturnsCreatedResultWithCustomer()
    {
        // Arrange
        var newCustomer = new Customer { FirstName = "John", LastName = "Doe", Email = "John.Doe@yopmail.com" };
        var requestBody = new StringContent(JsonConvert.SerializeObject(newCustomer), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/customers", requestBody);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var createdCustomer = JsonConvert.DeserializeObject<Customer>(content);
        Assert.NotNull(createdCustomer);
        Assert.Equal(newCustomer.FirstName, createdCustomer.FirstName);
        Assert.Equal(newCustomer.LastName, createdCustomer.LastName);
    }

    [Fact]
    public async Task UpdateCustomer_ExistingId_ReturnsNoContentResult()
    {
        // Arrange
        var existingId = 1;
        var updatedCustomer = new Customer { Id = 1, FirstName = "Updated", LastName = "Doe", Email = "John.Doe@yopmail.com" };
        var requestBody = new StringContent(JsonConvert.SerializeObject(updatedCustomer), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync($"/api/customers/{existingId}", requestBody);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var nonExistingId = 999;
        var updatedCustomer = new Customer { Id = 999, FirstName = "Updated", LastName = "Doe", Email = "John.Doe@yopmail.com" };
        var requestBody = new StringContent(JsonConvert.SerializeObject(updatedCustomer), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync($"/api/customers/{nonExistingId}", requestBody);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_ExistingId_ReturnsNoContentResult()
    {
        // Arrange
        long existingId = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/customers/{existingId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_NonExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var nonExistingId = 999;

        // Act
        var response = await _client.DeleteAsync($"/api/customers/{nonExistingId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
