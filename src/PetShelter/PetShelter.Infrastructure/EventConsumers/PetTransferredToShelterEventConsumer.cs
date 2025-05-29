using BuildingBlocks.RabbitMq.Events;
using MassTransit;
using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.EventConsumers;

public class PetTransferredToShelterEventConsumer(IPetRepository petRepository, IUnitOfWork unitOfWork) : IConsumer<PetTransferredToShelterIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PetTransferredToShelterIntegrationEvent> context)
    {
        var pet = await petRepository.GetByIdAsync(context.Message.PetId);

        if (pet == null)
        {
            throw new InvalidOperationException("Pet not found");
        }
        
        pet.TransferToShelterFromHospital();
        
        petRepository.Update(pet);
        
        await unitOfWork.SaveAsync();
    }
}