using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Abstractions;

public interface IProductRepository
{
   Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
   Task AddAsync(Product product, CancellationToken cancellationToken = default);
   void Update(Product product, CancellationToken cancellationToken = default);
}