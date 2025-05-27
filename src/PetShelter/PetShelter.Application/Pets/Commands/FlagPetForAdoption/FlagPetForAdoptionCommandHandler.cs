using BuildingBlocks.Common.CQRS;
using BuildingBlocks.Common.Exceptions;
using PetShelter.Domain.Entities;
using PetShelter.Domain.Repositories;

namespace PetShelter.Application.Pets.Commands.FlagPetForAdoption;

public class FlagPetForAdoptionCommandHandler(IPetRepository petRepository, IUnitOfWork unitOfWork) : ICommandHandler<FlagPetForAdoptionCommand>
{
    public async Task Handle(FlagPetForAdoptionCommand request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetByIdAsync(request.PetId, cancellationToken);
        
        if(pet is null)
        {
            throw new ResourceNotFound(nameof(Pet), request.PetId.ToString());
        }
        
        pet.FlagForAdoption();
        
        petRepository.Update(pet, cancellationToken);
        
        await unitOfWork.SaveAsync(cancellationToken);
    }
}