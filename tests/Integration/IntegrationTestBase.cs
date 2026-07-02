using System.Data.Common;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;

namespace Integration;

public abstract class IntegrationTestBase(IntegrationTestWebAppFactory factory) : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
{
    private Respawner _respawner = null!;
    private DbConnection _connection = null!;

    protected HttpClient Client { get; private set; } = null!;
    protected IServiceScope Scope { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Client = factory.CreateClient();
        Scope = factory.Services.CreateScope();
        
        var dbContext = Scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        await dbContext.Database.MigrateAsync();
        
        // Set up respawn
        _connection = new NpgsqlConnection(factory.ConnectionString);
        await _connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["catalog"]
        });
    }

    public async Task DisposeAsync()
    {
        await _respawner.ResetAsync(_connection);
        await _connection.CloseAsync();
        Scope.Dispose();
    }
}