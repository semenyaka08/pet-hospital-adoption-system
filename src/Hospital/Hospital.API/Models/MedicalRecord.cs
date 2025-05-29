namespace Hospital.API.Models;

public class MedicalRecord
{
    public Guid Id { get; set; }

    public Guid PetId { get; set; }

    public string PetName { get; set; } = string.Empty;
    
    public string Species { get; set; } = string.Empty;
    
    public string Reason { get; set; } = string.Empty;
    
    public string VeterinarianNotes { get; set; } = string.Empty;

    public bool TreatmentStarted { get; set; }
    
    public Guid? DoctorId { get; set; }
    
    public Doctor? Doctor { get; set; }
}