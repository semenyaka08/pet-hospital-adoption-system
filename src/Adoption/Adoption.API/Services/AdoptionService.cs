using BuildingBlocks.RabbitMq.Events;
using MassTransit;
using PetShelter.Grpc;

namespace Adoption.API.Services;

public class AdoptionService(IReservationService reservationService, PetService.PetServiceClient petServiceClient, IPublishEndpoint publishEndpoint) : IAdoptionService
{
    public async Task<string> AdoptAsync(Guid petId, string userPhone)
    {
        var reservationResponse = await reservationService.GetReservationAsync(petId, userPhone);

        if (!reservationResponse.IsReserved)
        {
            return await AdoptionForNotReservedPetAsync(petId);
        }
        
        if(reservationResponse.IsReserved && reservationResponse.ReserverPhone == userPhone)
        {
            return await AdoptionForReservedPetAsync(petId);
        }
        
        if (reservationResponse.IsReserved && reservationResponse.ReserverPhone != userPhone)
        {
            throw new InvalidOperationException("This pet is already reserved by another user.");
        }
        
        return "Adoption cannot be completed.";
    }

    private async Task<string> AdoptionForReservedPetAsync(Guid petId)
    {
        var petAdoptedEvent = new PetAdoptedIntegrationEvent
        {
            PetId = petId,
        };
        
        await publishEndpoint.Publish(petAdoptedEvent);
        
        return "Adoption was completed successfully, reservation was found in redis storage.";
    }
    
    private async Task<string> AdoptionForNotReservedPetAsync(Guid petId)
    {
        var pet = await petServiceClient.GetPetByIdAsync(new GetPetByIdRequest { PetId = petId.ToString() });

        if (!pet.IsAvailableForAdoption)
        {
            throw new InvalidOperationException("This pet is not available for adoption.");
        }

        var petAdoptedEvent = new PetAdoptedIntegrationEvent
        {
            PetId = petId
        };
        
        await publishEndpoint.Publish(petAdoptedEvent);

        return "Adoption was completed successfully, no reservation was found in redis storage.";
    }
}