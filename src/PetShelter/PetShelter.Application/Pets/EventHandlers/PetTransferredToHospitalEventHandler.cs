using MediatR;
using PetShelter.Domain.DomainEvents;

namespace PetShelter.Application.Pets.EventHandlers;

public class PetTransferredToHospitalEventHandler : INotificationHandler<PetTransferredToHospitalDomainEvent>
{
    public Task Handle(PetTransferredToHospitalDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}