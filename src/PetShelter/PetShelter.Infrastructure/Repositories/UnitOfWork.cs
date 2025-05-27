using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}