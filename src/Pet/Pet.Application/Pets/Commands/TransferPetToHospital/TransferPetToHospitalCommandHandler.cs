using BuildingBlocks.Common.CQRS;
using BuildingBlocks.Common.Exceptions;
using Pet.Domain.Repositories;

namespace Pet.Application.Pets.Commands.TransferPetToHospital;

public class TransferPetToHospitalCommandHandler(IPetRepository petRepository, IUnitOfWork unitOfWork) : ICommandHandler<TransferPetToHospitalCommand>
{
    public async Task Handle(TransferPetToHospitalCommand request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetByIdAsync(request.PetId, cancellationToken);
        
        if (pet is null)
        {
            throw new ResourceNotFound(nameof(Pet), request.PetId.ToString());
        }
        
        pet.TransferToHospital(request.Reason, request.Notes);
        
        petRepository.Update(pet, cancellationToken);
        
        await unitOfWork.SaveAsync(cancellationToken);
    }
}