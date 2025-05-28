using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.Repositories;

public class UnitOfWork(PetDbContext context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}