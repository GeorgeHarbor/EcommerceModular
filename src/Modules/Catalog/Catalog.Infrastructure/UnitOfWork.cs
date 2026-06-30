using Catalog.Application.Abstractions;

namespace Catalog.Infrastructure;

internal sealed class UnitOfWork(CatalogDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}