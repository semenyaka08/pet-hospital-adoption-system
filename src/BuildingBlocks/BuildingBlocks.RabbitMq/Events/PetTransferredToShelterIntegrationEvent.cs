namespace BuildingBlocks.RabbitMq.Events;

public class PetTransferredToShelterIntegrationEvent : IntegrationEvent
{
    public Guid PetId { get; set; }
}