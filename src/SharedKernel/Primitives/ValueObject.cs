
namespace SharedKernel.Primitives;

public abstract class ValueObject
{
  protected abstract IEnumerable<object> GetAtomicValues();

  public override bool Equals(object? obj)
  {
    if (obj is null || obj.GetType() != GetType())
      return false;

    return GetAtomicValues()
      .SequenceEqual(((ValueObject)obj).GetAtomicValues());
  }

  public override int GetHashCode() =>
    GetAtomicValues()
      .Aggregate(default(int), HashCode.Combine);

  public static bool operator ==(ValueObject? left, ValueObject? right) =>
    Equals(left, right);

  public static bool operator !=(ValueObject? left, ValueObject? right) =>
    !Equals(left, right);
}

