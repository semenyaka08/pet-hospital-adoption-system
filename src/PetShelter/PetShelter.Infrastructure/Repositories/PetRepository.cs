using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    public Task<Domain.Entities.Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(Domain.Entities.Pet pet, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}