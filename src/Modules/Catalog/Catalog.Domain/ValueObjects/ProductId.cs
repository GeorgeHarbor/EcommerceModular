// Namespace grouping the Catalog module's value objects.
namespace Catalog.Domain.ValueObjects;

// Strongly-typed product identifier wrapping a Guid (record gives value equality for free).
public record ProductId(Guid Value)
{
  // Creates a new ProductId backed by a freshly generated Guid.
  public static ProductId New() => new(Guid.NewGuid());
  // Represents an empty/uninitialized ProductId (all-zero Guid).
  public static ProductId Empty => new(Guid.Empty);
  // Renders the id as the underlying Guid's string form.
  public override string ToString() => Value.ToString();
}
