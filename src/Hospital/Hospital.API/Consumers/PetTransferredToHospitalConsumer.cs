using BuildingBlocks.RabbitMq.Events;
using MassTransit;

namespace Hospital.API.Consumers;

public class PetTransferredToHospitalConsumer : IConsumer<PetTransferredToHospitalIntegrationEvent>
{
    public Task Consume(ConsumeContext<PetTransferredToHospitalIntegrationEvent> context)
    {
        Console.WriteLine($"Pet transferred to hospital: {context.Message.PetId}; Pet Name: {context.Message.PetName}");
        
        return Task.CompletedTask;
    }
}