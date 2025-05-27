namespace PetShelter.Domain.Abstractions;

public interface IAggregate
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    
    void AddDomainEvent(IDomainEvent domainEvent);
    
    IDomainEvent[] ClearDomainEvents();
}