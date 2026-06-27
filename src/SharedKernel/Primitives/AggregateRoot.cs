namespace SharedKernel.Primitives
{
  public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
  {
  }
}
