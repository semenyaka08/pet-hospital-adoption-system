using PetShelter.Domain.Abstractions;

namespace PetShelter.Domain.DomainEvents;

public class PetFlaggedForAdoptionDomainEvent(Guid petId, string name, string breed) : IDomainEvent
{
    public Guid PetId { get; set; } = petId;

    public string Name { get; set; } = name;

    public string Breed { get; set; } = breed;
}