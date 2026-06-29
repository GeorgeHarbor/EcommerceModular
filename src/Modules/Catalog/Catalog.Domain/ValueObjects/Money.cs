// Imports the ValueObject base class for value-based equality.
using SharedKernel.Primitives;

// Namespace grouping the Catalog module's value objects.
namespace Catalog.Domain.ValueObjects;

// Money value object: a monetary amount paired with a currency, compared by value.
public class Money : ValueObject
{
  // The monetary amount (read-only after construction).
  public decimal Amount { get; }
  // The currency code (read-only after construction).
  public string Currency { get; }

  // Private constructor forces creation through the Create factory so validation runs.
  private Money(decimal amount, string currency)
  {
    // Store the amount.
    Amount = amount;
    // Store the currency.
    Currency = currency;
  }

  // Factory that validates inputs and returns a new Money instance.
  public static Money Create(decimal amount, string currency)
  {
    // Reject negative amounts.
    if (amount < 0)
      throw new ArgumentException("Amount cannot be negative", nameof(amount));

    // Reject blank or missing currency.
    if (string.IsNullOrWhiteSpace(currency))
      throw new ArgumentException("Currency cannot be empty", nameof(currency));

    // Return the validated Money value.
    return new Money(amount, currency);
  }

  // Supplies the values used for equality comparison (amount then currency).
  protected override IEnumerable<object> GetAtomicValues()
  {
    // Yield the amount as the first comparison value.
    yield return Amount;
    // Yield the currency as the second comparison value.
    yield return Currency;
  }
}
