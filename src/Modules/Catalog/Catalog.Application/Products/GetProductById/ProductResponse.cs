namespace Catalog.Application.Products.GetProductById;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    bool IsActive);