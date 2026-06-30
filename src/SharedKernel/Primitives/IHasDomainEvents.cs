namespace SharedKernel.Primitives;

public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}