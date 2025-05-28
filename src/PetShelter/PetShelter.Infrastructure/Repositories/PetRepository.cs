using Microsoft.EntityFrameworkCore;
using PetShelter.Domain.Entities;
using PetShelter.Domain.Repositories;
using PetShelter.Domain.ValueObjects;

namespace PetShelter.Infrastructure.Repositories;

public class PetRepository(PetDbContext context) : IPetRepository
{
    public async Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pet = await context.Pets.FirstOrDefaultAsync(cancellationToken);
        
        Console.WriteLine(pet!.Id.Value == id);
        
        return await context.Pets.FirstOrDefaultAsync(z=> z.Id == PetId.Of(id), cancellationToken);
    }

    public void Update(Pet pet, CancellationToken cancellationToken = default)
    {
        context.Pets.Update(pet);
    }
}