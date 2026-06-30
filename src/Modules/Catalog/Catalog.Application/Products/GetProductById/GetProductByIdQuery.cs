using MediatR;
using SharedKernel.Results;

namespace Catalog.Application.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<Result<ProductResponse>>;