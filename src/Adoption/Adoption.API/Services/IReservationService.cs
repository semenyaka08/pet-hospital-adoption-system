namespace Adoption.API.Services;

public interface IReservationService
{
    Task<ReserveData> ReserveAsync(Guid petId, string userPhone);

    Task<GetReservationResponse> GetReservationAsync(Guid petId, string userPhone);
}

public record GetReservationResponse(bool IsReserved, string ReserverPhone = "");

public record ReserveData(Guid PetId, string UserPhone);