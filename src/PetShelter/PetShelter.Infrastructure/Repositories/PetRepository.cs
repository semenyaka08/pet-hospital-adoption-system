using Microsoft.EntityFrameworkCore;
using PetShelter.Domain.Entities;
using PetShelter.Domain.Repositories;

namespace PetShelter.Infrastructure.Repositories;

public class PetRepository(PetDbContext context) : IPetRepository
{
    public async Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Pets.FirstOrDefaultAsync(z=> z.Id.Value == id, cancellationToken);
    }

    public void Update(Pet pet, CancellationToken cancellationToken = default)
    {
        context.Pets.Update(pet);
    }
}