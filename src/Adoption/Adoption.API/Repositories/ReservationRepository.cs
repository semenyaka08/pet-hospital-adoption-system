using System.Text.Json;
using Adoption.API.Models;
using StackExchange.Redis;

namespace Adoption.API.Repositories;

public class ReservationRepository(IConnectionMultiplexer multiplexer) : IReservationRepository
{
    private readonly IDatabase _database = multiplexer.GetDatabase();
    private const int ReservationTtlMinutes = 3;
    
    public async Task<bool> ReserveAsync(string petId, string userPhone)
    {
        var key = GetReservationKey(petId);
        var value = JsonSerializer.Serialize(new Reservation
        {
            PetId = petId,
            UserPhone = userPhone,
            ReservedAt = DateTime.UtcNow
        });
        
        return await _database.StringSetAsync(key, value, TimeSpan.FromMinutes(ReservationTtlMinutes), When.NotExists);
    }
    

    public async Task<string?> GetReserverPhoneAsync(string petId)
    {
        var key = GetReservationKey(petId);
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty) return null;

        var res = JsonSerializer.Deserialize<Reservation>(value!);
        return res?.UserPhone;
    }
    
    private static string GetReservationKey(string petId) => $"reservation:{petId}";
}