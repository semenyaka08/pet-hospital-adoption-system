namespace Pet.Domain.Repositories;

public interface IPetRepository
{
    Task<Entities.Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void Update(Entities.Pet pet, CancellationToken cancellationToken = default);
}