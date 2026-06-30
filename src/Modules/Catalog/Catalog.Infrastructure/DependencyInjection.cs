using Catalog.Application.Abstractions;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
     public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
     {
          var connectionString = configuration.GetConnectionString("Database")
                                 ?? throw new InvalidOperationException("Connection string 'Database' not found");

          services.AddDbContext<CatalogDbContext>(options =>
              options.UseNpgsql(connectionString));

          services.AddScoped<IProductRepository, EfCoreProductRepository>();
          services.AddScoped<IUnitOfWork, UnitOfWork>();

          return services;
     }
}