using Catalog.Application.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using MediatR;
using SharedKernel.Results;

namespace Catalog.Application.Products.Commands.CreateProduct;

internal sealed class CreateProductHandler(
    IProductRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var price = Money.Create(command.Price, command.Currency);

        var result = Product.Create(command.Name, price);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Error);

        await _repository.AddAsync(result.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(result.Value.Id.Value);
    }
}