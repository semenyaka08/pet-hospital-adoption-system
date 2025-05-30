﻿using BuildingBlocks.Common.CQRS;
using MediatR;

namespace PetShelter.Application.Pets.Commands.FlagPetForAdoption;

public record FlagPetForAdoptionCommand(Guid PetId) : ICommand;