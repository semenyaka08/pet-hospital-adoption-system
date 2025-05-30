using Grpc.Core;
using PetShelter.Domain.Repositories;
using PetShelter.Grpc;

namespace PetShelter.API.Services;

public class PetServiceGrpc(IPetRepository petRepository) : PetService.PetServiceBase
{
    public override async Task<GetPetByIdResponse> GetPetById(GetPetByIdRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.PetId, out var petId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid PetId GUID format."));
        }
        
        var pet = await petRepository.GetByIdAsync(petId, context.CancellationToken);
        
        if (pet is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Pet with ID {petId} not found."));
        }

        return new GetPetByIdResponse
        {
            PetId = pet.Id.Value.ToString(),
            Name = pet.Name.Value,
            Status = pet.BusinessState.Status.ToString()
        };
    }
}