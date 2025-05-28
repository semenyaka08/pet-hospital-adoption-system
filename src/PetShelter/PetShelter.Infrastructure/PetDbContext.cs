using Microsoft.EntityFrameworkCore;
using PetShelter.Domain.Entities;
using PetShelter.Infrastructure.Configurations;

namespace PetShelter.Infrastructure;

public class PetDbContext(DbContextOptions<PetDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}