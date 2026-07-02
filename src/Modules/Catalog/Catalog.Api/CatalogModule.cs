using Catalog.Api.Endpoints;
using Catalog.Application;
using Catalog.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Catalog.Api;

public sealed class CatalogModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddCatalogApplication();
        services.AddCatalogInfrastructure(config);
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapProductEndpoints();
    }
}