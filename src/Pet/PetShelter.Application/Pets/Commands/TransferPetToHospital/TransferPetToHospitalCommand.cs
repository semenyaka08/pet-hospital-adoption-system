using BuildingBlocks.Common.CQRS;

namespace PetShelter.Application.Pets.Commands.TransferPetToHospital;

public record TransferPetToHospitalCommand(Guid PetId, string Reason, string Notes = "") : ICommand;