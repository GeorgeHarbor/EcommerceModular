// Imports the Error type used to define these product errors.
using SharedKernel.Results;

// Namespace grouping the Catalog module's value objects (where these errors live).
namespace Catalog.Domain.Errors;

// Static catalog of predefined errors related to Product operations.
public static class ProductErrors
{
  // Builds a "not found" error for a specific product id (method because the message varies by id).
  public static Error NotFound(Guid id) =>
    new("Product.NotFound", $"Product with ID '{id}' was not found");

  // Error returned when a product name is empty or blank.
  public static readonly Error NameEmpty =
    new("Product.NameEmpty", "Product name cannot be empty");

  // Error returned when a product price is not greater than zero.
  public static readonly Error InvalidPrice =
    new("Product.InvalidPrice", "Product price must be greater than zero");

  // Error returned when deactivating a product that is already deactivated.
  public static readonly Error AlreadyDeactivated =
    new("Product.AlreadyDeactivated", "Product is already deactivated");
}
