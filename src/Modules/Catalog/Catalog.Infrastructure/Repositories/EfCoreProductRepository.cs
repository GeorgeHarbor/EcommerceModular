using Catalog.Application.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

internal sealed class EfCoreProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default) =>
        await context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default) =>
            await context.Products.AddAsync(product, cancellationToken);

        public void Update(Product product, CancellationToken cancellationToken = default)
        {
            context.Products.Update(product);
        }
}