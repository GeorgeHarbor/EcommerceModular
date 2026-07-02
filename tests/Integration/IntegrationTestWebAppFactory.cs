using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Catalog.Infrastructure;

namespace Integration;

public sealed class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:16")
        .WithDatabase("ecommerce_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();
    
    public string ConnectionString => _postgres.GetConnectionString();
    
    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptors = services
                .Where(d => d.ServiceType.IsGenericType &&
                            d.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>))
                .ToList();
            
            foreach (var descriptor in descriptors)
                services.Remove(descriptor);

            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(_postgres.GetConnectionString());
            });
        });
    }

    public new async Task DisposeAsync()
    {
        await _postgres.StopAsync();
    }
}