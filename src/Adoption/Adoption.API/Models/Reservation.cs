namespace Adoption.API.Models;

public class Reservation
{
    public required string PetId { get; set; }
    
    public required string UserPhone { get; set; }
    
    public required DateTime ReservedAt { get; set; }
}