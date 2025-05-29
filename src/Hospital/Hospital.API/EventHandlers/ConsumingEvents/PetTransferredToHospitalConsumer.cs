using BuildingBlocks.RabbitMq.Events;
using Hospital.API.Infrastructure;
using Hospital.API.Models;
using MassTransit;

namespace Hospital.API.EventHandlers.ConsumingEvents;

public class PetTransferredToHospitalConsumer(HospitalDbContext dbContext) : IConsumer<PetTransferredToHospitalIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PetTransferredToHospitalIntegrationEvent> context)
    {
        var medicalRecord = new MedicalRecord
        {
            Id = Guid.NewGuid(),
            PetId = context.Message.PetId,
            PetName = context.Message.PetName,
            Species = context.Message.Species,
            Reason = context.Message.Reason,
            VeterinarianNotes = context.Message.VeterinarianNotes
        };
        
        await dbContext.MedicalRecords.AddAsync(medicalRecord);
        await dbContext.SaveChangesAsync();
        
        //Implement doctors notifying logic about the new medical record
    }
}