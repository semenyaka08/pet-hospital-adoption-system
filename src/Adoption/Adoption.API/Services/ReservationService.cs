using Adoption.API.Repositories;

namespace Adoption.API.Services;

public class ReservationService(IReservationRepository repository) : IReservationService
{
    public async Task<ReserveData> ReserveAsync(Guid petId, string userPhone)
    {
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