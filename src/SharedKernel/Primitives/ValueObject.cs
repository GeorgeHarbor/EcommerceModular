// Declares the namespace that groups the core domain building blocks.
namespace SharedKernel.Primitives;

// Base class for value objects: objects compared by their values rather than by identity.
public abstract class ValueObject
{
  // Derived types return the sequence of values that define equality (e.g. amount + currency).
  protected abstract IEnumerable<object> GetAtomicValues();

  // Overrides the default equality check so two value objects are equal when their atomic values match.
  public override bool Equals(object? obj)
  {
    // Not equal if the other object is null or is a different concrete type.
    if (obj is null || obj.GetType() != GetType())
      return false;

    // Equal only if every atomic value matches, in the same order, between the two objects.
    return GetAtomicValues()
      .SequenceEqual(((ValueObject)obj).GetAtomicValues());
  }

  // Builds a hash code by combining the hash of each atomic value (keeps Equals and GetHashCode consistent).
  public override int GetHashCode() =>
    GetAtomicValues()
      .Aggregate(default(int), HashCode.Combine);

  // Enables the == operator by delegating to the static Equals (which handles nulls safely).
  public static bool operator ==(ValueObject? left, ValueObject? right) =>
    Equals(left, right);

  // Enables the != operator as the logical negation of ==.
  public static bool operator !=(ValueObject? left, ValueObject? right) =>
    !Equals(left, right);
}
