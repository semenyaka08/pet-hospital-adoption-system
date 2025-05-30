using Adoption.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adoption.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService reservationService) : ControllerBase
{
    [HttpPost("reservations")]
    public async Task<IActionResult> ReservePetAsync(ReservePetRequest request)
    {
        if (request.PetId == Guid.Empty)
        {
            return BadRequest("Pet ID cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return BadRequest("Phone number cannot be empty.");
        }

        var reservationData = await reservationService.ReserveAsync(request.PetId, request.PhoneNumber);
        
        return Ok(reservationData);
    }
}

public record ReservePetRequest(Guid PetId, string PhoneNumber);