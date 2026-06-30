using MediatR;
using SharedKernel.Results;

namespace Catalog.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Currency) : IRequest<Result<Guid>>;