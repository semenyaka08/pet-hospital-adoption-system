using BuildingBlocks.RabbitMq.Events;
using Hospital.API.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController(HospitalDbContext context, IPublishEndpoint endpoint) : ControllerBase
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
        
        medicalRecords.TreatmentIsOngoing = true;
        
        context.MedicalRecords.Update(medicalRecords);
        
        await context.SaveChangesAsync();
        
        return Ok("Treatment started successfully.");
    }

    [HttpPost("complete-treatment")]
    public async Task<IActionResult> CompleteTreatment([FromQuery] Guid medicalRecordId)
    {
        var medicalRecord = await context.MedicalRecords.FirstOrDefaultAsync(z => z.Id == medicalRecordId);
        
        if (medicalRecord == null)
        {
            return NotFound("Medical record not found.");
        }
        
        medicalRecord.TreatmentIsOngoing = false;
        
        context.MedicalRecords.Update(medicalRecord);
        
        await context.SaveChangesAsync();

        var petTransferredToShelterEvent = new PetTransferredToShelterIntegrationEvent
        {
            PetId = medicalRecord.PetId
        };
        
        await endpoint.Publish(petTransferredToShelterEvent);

        return Ok("Treatment completed successfully.");
    }
}