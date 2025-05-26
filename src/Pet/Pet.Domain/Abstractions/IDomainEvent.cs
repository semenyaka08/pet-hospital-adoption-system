using MediatR;

namespace Pet.Domain.Abstractions;

public interface IDomainEvent : INotification 
{
    Guid EventId => Guid.NewGuid();
    
    DateTime OccurredOn => DateTime.UtcNow;
    
    string EventType => GetType().Name;
}