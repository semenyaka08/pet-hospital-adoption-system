using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Application.Pets.Commands.TransferPetToHospital;

namespace PetShelter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(ISender sender) : ControllerBase
{
    [HttpPost("{petId}/transfer-to-hospital")]
    public async Task<IActionResult> TransferToHospitalAsync(Guid petId, [FromBody] TransferPetToHospitalRequest request, CancellationToken cancellationToken)
    {
        var command = new TransferPetToHospitalCommand(petId, request.Reason, request.Notes);
        
        await sender.Send(command, cancellationToken);
        
        return Ok("Pet will be sent to hospital as soon as possible.");
    }
}

public record TransferPetToHospitalRequest(string Reason, string Notes = "");