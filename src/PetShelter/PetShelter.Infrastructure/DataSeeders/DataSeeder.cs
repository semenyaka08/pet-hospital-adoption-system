using PetShelter.Domain.Entities;
using PetShelter.Domain.Enums;
using PetShelter.Domain.ValueObjects;

namespace PetShelter.Infrastructure.DataSeeders;

public class DataSeeder(PetDbContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if(!context.Pets.Any())
            await SeedPetsAsync();
    }
    
    private async Task SeedPetsAsync()
    {
        var petId = PetId.Of(Guid.NewGuid());
        var name = Name.Of("Buddy");
        
        List<Pet> pets = [
            Pet.Create(PetId.Of(Guid.NewGuid()),
            Name.Of("Buddy"),
            Species.Of("Dog"),
            Breed.Of("Golden Retriever"),
            Sex.Male,
            BusinessState.Of(
                status: Status.Rescued,
                isAvailableForAdoption: true,
                rescuedDate: DateTime.UtcNow.AddMonths(-2)
            ),
            PhysicalCharacteristics.Of(30.5m, 55.0m, true, true),
            dateOfBirth: DateOfBirth.Of(new DateTime(2021, 5, 10))),
            Pet.Create(PetId.Of(Guid.NewGuid()), 
                Name.Of("Mittens"),
                Species.Of("Cat"),
                Breed.Of("Siberian"),
                Sex.Female,
                BusinessState.Of(
                    status: Status.Rescued,
                    isAvailableForAdoption: true,
                    rescuedDate: DateTime.UtcNow.AddMonths(-3)
                ),
                PhysicalCharacteristics.Of(4.5m, 25.0m, false, true),
                dateOfBirth: DateOfBirth.Of(new DateTime(2022, 2, 15))
                ),
            Pet.Create(PetId.Of(Guid.NewGuid()),
                Name.Of("Spike"),
                Species.Of("Dog"),
                Breed.Of("Bulldog"),
                Sex.Male,
                BusinessState.Of(
                    status: Status.Rescued,
                    isAvailableForAdoption: false,
                    rescuedDate: DateTime.UtcNow.AddMonths(-5),
                    adoptedDate: DateTime.UtcNow.AddMonths(-1)
                ),
                PhysicalCharacteristics.Of(24.5m, 40.0m, true, true),
                dateOfBirth: DateOfBirth.Of(new DateTime(2020, 8, 20)))
        ];
        
        await context.Pets.AddRangeAsync(pets);
        await context.SaveChangesAsync();
    }
}