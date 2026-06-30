using Catalog.Application.Abstractions;
using MediatR;
using SharedKernel.Primitives;

namespace Catalog.Infrastructure;

internal sealed class UnitOfWork(CatalogDbContext context, IPublisher publisher) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = CollectDomainEvents();
        
        var result = await context.SaveChangesAsync(cancellationToken);
        
        await DispatchDomainEventsAsync(domainEvents, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task DispatchDomainEventsAsync(List<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }

    private List<IDomainEvent> CollectDomainEvents()
    {
        var entitiesWithEvents = context.ChangeTracker
            .Entries()
            .Select(e => e.Entity)
            .OfType<IHasDomainEvents>()
            .Where(e => e.GetDomainEvents().Count != 0)
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.GetDomainEvents())
            .ToList();
        
        entitiesWithEvents.ForEach(e => e.ClearDomainEvents());
        return domainEvents;
    }
}