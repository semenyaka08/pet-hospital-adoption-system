namespace BuildingBlocks.RabbitMq.Events;

public class PetTransferredToHospitalIntegrationEvent : IntegrationEvent
{
    public Guid PetId { get; set; }
    
    public string PetName { get; set; } = string.Empty;
    
    public string Species { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public string VeterinarianNotes { get; set; } = string.Empty;
}