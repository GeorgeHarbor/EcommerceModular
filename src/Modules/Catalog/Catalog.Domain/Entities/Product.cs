// Imports the ProductCreated domain event type.

using Catalog.Domain.Errors;
using Catalog.Domain.Events;
// Imports the value objects used here (ProductId, Money).
using Catalog.Domain.ValueObjects;
// Imports the base primitives (AggregateRoot, etc.).
using SharedKernel.Primitives;
// Imports the Result/Error types for returning success or failure.
using SharedKernel.Results;

// Namespace grouping the Catalog module's domain entities.
namespace Catalog.Domain.Entities;

// The Product aggregate root, identified by a ProductId; sealed so it cannot be inherited.
public sealed class Product : AggregateRoot<ProductId>
{
    // Private constructor forces creation through the Create factory so invariants are enforced.
    private Product(
        ProductId id,    // The product's unique identifier, passed to the base aggregate root.
        string name,     // The product's display name.
        Money price) : base(id)
    {
        // Assign the name passed in.
        Name = name;
        // Assign the price passed in.
        Price = price;
    }
    
#pragma warning disable CS8618
private Product() : base(default!)
{
}
#pragma warning restore CS8618

    // The product name; readable publicly, changeable only inside this class.
    public string Name { get; private set; } = string.Empty;
    // The product price as a Money value object; changeable only inside this class.
    public Money Price { get; private set; }
    // Whether the product is currently active/available.
    public bool IsActive { get; private set; }

    // Factory method that validates input and returns either a created Product or an error.
    public static Result<Product> Create(string name, Money price)
    {
        // Reject names that are null, empty, or only whitespace.
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Failure(ProductErrors.NameEmpty);

        // Build a new product with a freshly generated id and mark it active.
        var product = new Product(ProductId.New(), name, price)
        {
            IsActive = true
        };

        // Record a domain event capturing that a product was created (id, name, amount, currency).
        product.RaiseDomainEvent(new ProductCreated(
            product.Id.Value,
            product.Name,
            product.Price.Amount,
            product.Price.Currency));

        // Return the successfully created product.
        return Result<Product>.Success(product);
    }

    // Changes the product name, validating it is not blank.
    public Result UpdateName(string name)
    {
        // Reject blank names and return a failure error.
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(ProductErrors.NameEmpty);
        // Apply the new name.
        Name = name;
        // Report success.
        return Result.Success();
    }

    // Changes the product price, validating it is positive.
    public Result UpdatePrice(Money price)
    {
        // Reject prices that are zero or negative.
        if (price.Amount <= 0)
            return Result.Failure(ProductErrors.InvalidPrice);
        // Apply the new price.
        Price = price;
        // Report success.
        return Result.Success();
    }

    // Marks the product inactive, failing if it is already inactive.
    public Result Deactivate()
    {
        // Cannot deactivate something that is already deactivated.
        if (!IsActive)
            return Result.Failure(ProductErrors.AlreadyDeactivated);
        // Flip the active flag off.
        IsActive = false;
        // Report success.
        return Result.Success();
    }
}
