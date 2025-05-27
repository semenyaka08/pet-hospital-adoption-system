using BuildingBlocks.Common.CQRS;
using MediatR;
using PetShelter.Domain.Repositories;

namespace PetShelter.Application.Pets.Commands.FlagPetForAdoption;

public class FlagPetForAdoptionCommandHandler(IPetRepository petRepository, IUnitOfWork unitOfWork) : ICommandHandler<FlagPetForAdoptionCommand>
{
    public async Task Handle(FlagPetForAdoptionCommand request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetByIdAsync(request.PetId, cancellationToken);
        
        pet.FlagForAdoption();
        
        petRepository.Update(pet, cancellationToken);
        
        await unitOfWork.SaveAsync(cancellationToken);
    }
}