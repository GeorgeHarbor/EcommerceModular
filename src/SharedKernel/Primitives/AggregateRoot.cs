// Declares the namespace that groups the core domain building blocks.
namespace SharedKernel.Primitives
{
  // Base class for aggregate roots (the main entry point of a cluster of related objects).
  // It inherits from Entity<TId>, passing the id through to the base constructor.
  public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
  {
    // No extra members for now; it exists to mark and distinguish aggregate roots from plain entities.
  }
}
