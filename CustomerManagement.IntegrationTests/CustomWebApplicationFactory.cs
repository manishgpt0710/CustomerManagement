using CustomerManagement.Data;
using CustomerManagement.IntegrationTests.MiddlewareHelpers;
using CustomerManagement.IntegrationTests.TestData.Factories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CustomerManagement.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory>
{

}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private bool _testDataInitialized = false;

    public IServiceScope CreateServiceScope()
    {
        return Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, config) =>
        {
            IEnumerable<KeyValuePair<string, string?>>? initialData = new[]
            {
                new KeyValuePair<string, string?>("x-api-key", "cmVhZC1vbmx5LWtleQ==")
            };
            config.AddInMemoryCollection(initialData);
        });

        builder.ConfigureServices(PreStartupConfigureServices);
        builder.ConfigureTestServices(PostStartupConfigureServices);
    }

    private void PreStartupConfigureServices(IServiceCollection services)
    {
        var d1 = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
        if (d1 != null)
        {
            services.Remove(d1);
        }

        UseInMemoryDatabase<ApplicationDbContext>(services);

        services.AddSingleton<IStartupFilter, CustomStartupFilter>();
    }

    private void PostStartupConfigureServices(IServiceCollection services)
    {
        // Any Post Startup configuration will come here
        services.AddAuthentication();
    }

    protected override void ConfigureClient(HttpClient client)
    {
        if (!_testDataInitialized)
        {
            using (var scope = CreateServiceScope())
            {
                var services = scope.ServiceProvider;
                TestCustomerFactory.CreateCustomers(services).Wait();
            }
            _testDataInitialized = true;
        }
    }

    private static void UseInMemoryDatabase<TContext>(IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDbForTesting");
            options.EnableSensitiveDataLogging();
        });
    }
}
