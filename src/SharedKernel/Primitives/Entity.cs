using System.Collections.Generic;

namespace SharedKernel.Primitives;

public abstract class Entity<TId>(TId id)
{
  public TId Id { get; private set; } = id;

  public readonly List<IDomainEvent> _domainEvents = [];

  public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents;

  public void ClearDomainEvents() => _domainEvents.Clear();

  protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
    _domainEvents.Add(domainEvent);
}

