using Microsoft.EntityFrameworkCore;
using Pet.Infrastructure.Configurations;
using DomainPet = Pet.Domain.Entities.Pet;

namespace Pet.Infrastructure;

public class PetDbContext(DbContextOptions<PetDbContext> options) : DbContext(options)
{
    public DbSet<DomainPet> Pets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetEntityConfiguration<DomainPet>());
        
        base.OnModelCreating(modelBuilder);
    }
}