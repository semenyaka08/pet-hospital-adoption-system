using Adoption.API.Repositories;
using PetShelter.Grpc;

namespace Adoption.API.Services;

public class ReservationService(IReservationRepository repository, PetService.PetServiceClient petServiceClient) : IReservationService
{
    public async Task<ReserveData> ReserveAsync(Guid petId, string userPhone)
    {
        var pet = await petServiceClient.GetPetByIdAsync(new GetPetByIdRequest { PetId = petId.ToString() });

        if(string.IsNullOrWhiteSpace(pet.PetId))
        {
            throw new InvalidOperationException("this pet does not exist");
        }
        
        if (!pet.IsAvailableForAdoption)
        {
            throw new InvalidOperationException("this pet is not available for reservation");
        }
        
        var reservationResult = await repository.ReserveAsync(petId.ToString(), userPhone);
        
        if (!reservationResult)
        {
            throw new InvalidOperationException("this pet is already reserved");
        }
        
        return new ReserveData(PetId: petId, UserPhone: userPhone);
    }

    public async Task<GetReservationResponse> GetReservationAsync(Guid petId, string userPhone)
    {
        var reserverPhone = await repository.GetReserverPhoneAsync(petId.ToString());
        
        var response = new GetReservationResponse(
            IsReserved: reserverPhone != null,
            ReserverPhone: reserverPhone ?? string.Empty
        );
        
        return response;
    }
}