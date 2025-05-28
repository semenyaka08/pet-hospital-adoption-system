using MediatR;
using PetShelter.Domain.DomainEvents;

namespace PetShelter.Application.Pets.EventHandlers;

public class PetFlaggedForAdoptionEventHandler : INotificationHandler<PetFlaggedForAdoptionDomainEvent>
{
    public Task Handle(PetFlaggedForAdoptionDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}