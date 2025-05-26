namespace PHA.Domain.Abstractions;

public abstract class Aggregate<T> : Entity<T>, IAggregate
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly(); 

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        var queuedEvents = _domainEvents.ToArray();
        
        _domainEvents.Clear();
        
        return queuedEvents;
    }
}