using PetShelter.Domain.Abstractions;
using PetShelter.Domain.DomainEvents;
using PetShelter.Domain.Enums;
using PetShelter.Domain.ValueObjects;

namespace PetShelter.Domain.Entities;

public class Pet : Aggregate<PetId>
{
    public Name Name { get; set; } = default!;
    
    public Species Species { get; set; } = default!;
    
    public Breed Breed { get; set; } = default!;

    public Sex Sex { get; set; }

    public BusinessState BusinessState { get; set; } = default!;

    public PhysicalCharacteristics PhysicalCharacteristics { get; set; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
    
    private Pet() { }

    public static Pet Create(PetId petId, Name name, Species species, Breed breed, Sex sex, BusinessState businessState,
        PhysicalCharacteristics physicalCharacteristics, DateOfBirth dateOfBirth)
    {
        ArgumentNullException.ThrowIfNull(petId);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(species);
        ArgumentNullException.ThrowIfNull(breed);
        ArgumentNullException.ThrowIfNull(sex);
        ArgumentNullException.ThrowIfNull(businessState);
        ArgumentNullException.ThrowIfNull(physicalCharacteristics);
        ArgumentNullException.ThrowIfNull(dateOfBirth);

        return new Pet
        {
            Id = petId,
            Name = name,
            Species = species,
            Breed = breed,
            Sex = sex,
            BusinessState = businessState,
            PhysicalCharacteristics = physicalCharacteristics,
            DateOfBirth = dateOfBirth
        };
    }
    
    public void FlagForAdoption()
    {
        if (BusinessState.Status == Status.Adopted)
            throw new InvalidOperationException("Pet is already adopted");
        
        if (BusinessState.Status == Status.InHospital)
            throw new InvalidOperationException("Pet cannot be flagged for adoption while in hospital");
        
        if (!PhysicalCharacteristics.IsVaccinated)
            throw new InvalidOperationException("Pet must be vaccinated before adoption");
        
        BusinessState = BusinessState.Of(Status.Adopted, false, BusinessState.RescuedDate, DateTime.UtcNow);
        
        AddDomainEvent(new PetFlaggedForAdoptionDomainEvent(Id.Value, Name.Value, Species.Value));
    }
    
    public void TransferToHospital(string reason, string veterinarianNotes = "")
    {
        if(BusinessState.Status != Status.UnderCare || BusinessState.Status != Status.Rescued)
            throw new InvalidOperationException("Pet must be under care or rescued to be transferred to hospital");
    
        BusinessState = BusinessState.Of(Status.InHospital, false, BusinessState.RescuedDate);
        
        AddDomainEvent(new PetTransferredToHospitalDomainEvent(
            Id.Value, 
            Name.Value, 
            Species.Value, 
            reason, 
            veterinarianNotes)
        );
    }
}