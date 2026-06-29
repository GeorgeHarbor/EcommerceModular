// Declares the namespace that groups the core domain building blocks.
namespace SharedKernel.Primitives;

// Base class for all domain entities; generic over the type of the identifier (TId).
// The primary constructor takes the id and assigns it via the property initializer below.
public abstract class Entity<TId>(TId id)
{
  // The unique identifier of the entity; readable by everyone, settable only inside this class.
  public TId Id { get; private set; } = id;

  // Backing list that holds domain events raised by this entity; starts empty ([] is a collection expression).
  private readonly List<IDomainEvent> _domainEvents = [];

  // Exposes the raised domain events as a read-only list so callers cannot modify the internal list.
  public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents;

  // Removes all stored domain events (typically called after they have been dispatched).
  public void ClearDomainEvents() => _domainEvents.Clear();

  // Adds a new domain event to the internal list; only derived entities can call this.
  protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
    _domainEvents.Add(domainEvent);
}
