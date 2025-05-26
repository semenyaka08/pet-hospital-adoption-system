using System.Security.AccessControl;
using PHA.Domain.Abstractions;
using PHA.Domain.ValueObjects;

namespace PHA.Domain.DomainEvents;

public class PetTransferredToHospitalDomainEvent(Guid petId, string name, string species, string reason, string veterinarianNotes) : IDomainEvent
{
    public Guid PetId { get; set; } = petId;
    
    public string Name { get; set; } = name;
    
    public string Species { get; set; } = species;
    
    public string Reason { get; set; } = reason;
    
    public string VeterinarianNotes { get; set; } = veterinarianNotes;
}