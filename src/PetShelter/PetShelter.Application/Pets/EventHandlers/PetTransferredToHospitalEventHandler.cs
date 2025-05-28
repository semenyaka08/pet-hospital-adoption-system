using MassTransit;
using MediatR;
using PetShelter.Application.Extensions;
using PetShelter.Domain.DomainEvents;

namespace PetShelter.Application.Pets.EventHandlers;

public class PetTransferredToHospitalEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<PetTransferredToHospitalDomainEvent>
{
    public async Task Handle(PetTransferredToHospitalDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = notification.ToIntegrationEvent();
        
        await publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}