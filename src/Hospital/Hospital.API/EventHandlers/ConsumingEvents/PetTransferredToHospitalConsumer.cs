using BuildingBlocks.RabbitMq.Events;
using Hospital.API.Data.EmailServices;
using Hospital.API.Infrastructure;
using Hospital.API.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.EventHandlers.ConsumingEvents;

public class PetTransferredToHospitalConsumer(HospitalDbContext dbContext, IEmailService emailService)
    : IConsumer<PetTransferredToHospitalIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PetTransferredToHospitalIntegrationEvent> context)
    {
        var medicalRecord = new MedicalRecord
        {
            Id = Guid.NewGuid(),
            PetId = context.Message.PetId,
            PetName = context.Message.PetName,
            Species = context.Message.Species,
            Reason = context.Message.Reason,
            VeterinarianNotes = context.Message.VeterinarianNotes
        };

        await dbContext.MedicalRecords.AddAsync(medicalRecord);
        await dbContext.SaveChangesAsync();

        await NotifyDoctorsAsync(context.Message);
    }

    private async Task NotifyDoctorsAsync(PetTransferredToHospitalIntegrationEvent petInfo)
    {
        var doctors = await dbContext.Doctors.ToListAsync();

        if (!doctors.Any()) return;

        var doctorEmails = doctors.Select(d => d.Email).ToList();

        var subject = $"🏥 New Pet Admitted - {petInfo.PetName}";
        var body = EmailTemplateService.CreateEmailBody();

        foreach (var email in doctorEmails)
        {
            // Create veterinarian notes section if exists
            var vetNotesSection = !string.IsNullOrEmpty(petInfo.VeterinarianNotes)
                ? $@"<div style='background-color: #fff3cd; padding: 15px; border-radius: 8px; margin: 20px 0;'>
                    <h4>Veterinarian Notes:</h4>
                    <p>{petInfo.VeterinarianNotes}</p>
                </div>"
                : "";

            // Format the HTML with personalized data
            var personalizedHtml = string.Format(body,
                petInfo.PetName, // {0}
                petInfo.Species, // {1}
                petInfo.Reason, // {2}
                DateTime.UtcNow, // {3}
                vetNotesSection, // {4}
                petInfo.PetId, // {5}
                email // {6} - This personalizes the button for each doctor
            );

            try
            {
                await emailService.SendEmailAsync(email, subject, personalizedHtml);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email notifications: {ex.Message}");
            }
        }
    }
}