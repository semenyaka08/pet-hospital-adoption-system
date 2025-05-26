using BuildingBlocks.Common.CQRS;
using MediatR;

namespace Pet.Application.Pets.Commands.FlagPetForAdoption;

public record FlagPetForAdoptionCommand(Guid PetId) : ICommand;