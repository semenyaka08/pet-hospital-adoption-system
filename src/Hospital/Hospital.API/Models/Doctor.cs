using System.ComponentModel.DataAnnotations;

namespace Hospital.API.Models;

public class Doctor
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
}