using Hospital.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreatmentsController(HospitalDbContext context) : ControllerBase
{
    [HttpGet("start")]
    public async Task<IActionResult> StartTreatment([FromQuery] Guid petId, [FromQuery] string doctorEmail)
    {
        var medicalRecords = await context.MedicalRecords.FirstOrDefaultAsync(z=>z.PetId == petId);
        
        if (medicalRecords == null)
        {
            return NotFound("Medical records not found for the specified pet.");
        }
        
        var doctor = await context.Doctors.FirstOrDefaultAsync(z => z.Email == doctorEmail);
        
        if (doctor == null)
        {
            return NotFound("Doctor not found with the specified email.");
        }
        
        medicalRecords.DoctorId = doctor.Id;
        
        medicalRecords.TreatmentStarted = true;
        
        context.MedicalRecords.Update(medicalRecords);
        
        await context.SaveChangesAsync();
        
        return Ok("Treatment started successfully.");
    }
}