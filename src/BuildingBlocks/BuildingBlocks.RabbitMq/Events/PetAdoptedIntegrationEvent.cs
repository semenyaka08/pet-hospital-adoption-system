namespace BuildingBlocks.RabbitMq.Events;

public class PetAdoptedIntegrationEvent : IntegrationEvent
{
    public Guid PetId { get; set; }
}