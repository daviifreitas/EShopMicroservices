namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
{
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public readonly List<IDomainEvent> _domainEvents = new();
    
    public IDomainEvent[] ClearDomainEvents()
    {
        var domainEvents = _domainEvents.ToArray();
        _domainEvents.Clear();

        return domainEvents;
    }


    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}