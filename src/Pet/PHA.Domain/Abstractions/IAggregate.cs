namespace PHA.Domain.Abstractions;

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    
    void AddDomainEvent(IDomainEvent domainEvent);
    
    IDomainEvent[] ClearDomainEvents();
}