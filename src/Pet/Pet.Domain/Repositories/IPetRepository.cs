namespace Pet.Domain.Repositories;

public interface IPetRepository
{
    Task<Entities.Pet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(Entities.Pet pet, CancellationToken cancellationToken = default);
}