using BuildingBlocks.RabbitMq.Events;
using PetShelter.Domain.DomainEvents;

namespace PetShelter.Application.Extensions;

public static class PetEventMapper
{
    public static PetTransferredToHospitalIntegrationEvent ToIntegrationEvent(
        this PetTransferredToHospitalDomainEvent domainEvent)
    {
        return new PetTransferredToHospitalIntegrationEvent
        {
            PetId = domainEvent.PetId,
            PetName = domainEvent.Name,
            Species = domainEvent.Species,
            Reason = domainEvent.Reason,
            VeterinarianNotes = domainEvent.VeterinarianNotes
        };
    }
}