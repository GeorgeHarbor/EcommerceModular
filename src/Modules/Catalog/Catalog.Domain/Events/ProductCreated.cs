// Imports the IDomainEvent marker interface.
using SharedKernel.Primitives;

// Namespace grouping the Catalog module's domain events.
namespace Catalog.Domain.Events;

// Immutable record describing the "product was created" event; implements IDomainEvent so it can be raised.
public record ProductCreated(
    Guid ProductId,   // The id of the product that was created.
    string Name,      // The product's name at creation time.
    decimal Price,    // The product's price amount.
    string Currency   // The currency of the price.
    ) : IDomainEvent;
