using Catalog.Application.Abstractions;
using Catalog.Domain.Errors;
using Catalog.Domain.ValueObjects;
using MediatR;
using SharedKernel.Results;

namespace Catalog.Application.Products.GetProductById;

internal sealed class GetProductByIdHandler(IProductRepository _repository) : IRequestHandler<GetProductByIdQuery , Result<ProductResponse>>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(new ProductId(query.ProductId), cancellationToken);

        if (product is null)
            return Result<ProductResponse>.Failure(ProductErrors.NotFound(query.ProductId));
        var response = new ProductResponse(
            product.Id.Value,
            product.Name,
            product.Price.Amount,
            product.Price.Currency,
            product.IsActive);
        
        return Result<ProductResponse>.Success(response);
    }
}