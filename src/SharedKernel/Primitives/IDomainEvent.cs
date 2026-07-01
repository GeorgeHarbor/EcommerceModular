// Declares the namespace that groups the core domain building blocks.

using System.Transactions;
using MediatR;

namespace SharedKernel.Primitives;

// Marker interface implemented by every domain event; it carries no members and just tags a type as an event.
public interface IDomainEvent : INotification;
