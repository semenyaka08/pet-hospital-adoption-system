using BuildingBlocks.Common.CQRS;
using MediatR;
using Pet.Domain.Repositories;
using Pet.Domain.UnitOfWorkPattern;

namespace Pet.Application.Pets.Commands.FlagPetForAdoption;

public class FlagPetForAdoptionCommandHandler(IPetRepository petRepository, IUnitOfPattern pattern) : ICommandHandler<FlagPetForAdoptionCommand>
{
    public async Task Handle(FlagPetForAdoptionCommand request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetByIdAsync(request.PetId, cancellationToken);
        
        pet.FlagForAdoption();
        
        await petRepository.UpdateAsync(pet, cancellationToken);
        
        await pattern.SaveAsync(cancellationToken);
    }
}