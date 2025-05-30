using BuildingBlocks.RabbitMq.Events;
using MassTransit;
using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.EventConsumers;

public class PetAdoptedEventConsumer(IPetRepository petRepository, IUnitOfWork unitOfWork) : IConsumer<PetAdoptedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PetAdoptedIntegrationEvent> context)
    {
        var pet = await petRepository.GetByIdAsync(context.Message.PetId);

        if (pet == null)
        {
            throw new InvalidOperationException("Pet not found");
        }
        
        pet.FlagForAdoption();
        
        petRepository.Update(pet);
        
        await unitOfWork.SaveAsync();
    }
}