namespace Adoption.API.Repositories;

public interface IReservationRepository
{
    Task<bool> ReserveAsync(string petId, string userPhone);
    
    Task<string?> GetReserverPhoneAsync(string petId);
}