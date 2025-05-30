using Adoption.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adoption.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdoptionsController(IAdoptionService adoptionService) : ControllerBase
{
    [HttpPost("adopt/{petId}")]
    public async Task<IActionResult> AdoptPetAsync(Guid petId, [FromBody] string userPhone)
    {
        var adoptionResult = await adoptionService.AdoptAsync(petId, userPhone);
        
        return Ok(adoptionResult);
    }
}