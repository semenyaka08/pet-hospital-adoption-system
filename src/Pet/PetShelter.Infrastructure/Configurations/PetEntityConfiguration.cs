using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.Domain.ValueObjects;

namespace PetShelter.Infrastructure.Configurations;

public class PetEntityConfiguration<TPet> : IEntityTypeConfiguration<TPet>
    where TPet : Domain.Entities.Pet
{
    public void Configure(EntityTypeBuilder<TPet> builder)
    {
        builder.ToTable("Pets");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                petId => petId.Value,
                value => PetId.Of(value))
            .ValueGeneratedNever();
        
        builder.Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Of(value))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Species)
            .HasConversion(
                species => species.Value,
                value => Species.Of(value))
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.Breed)
            .HasConversion(
                breed => breed.Value,
                value => Breed.Of(value))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Sex)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(p => p.DateOfBirth)
            .HasConversion(
                dob => dob.Value,
                value => DateOfBirth.Of(value))
            .HasColumnType("date")
            .IsRequired();
        
        builder.OwnsOne(p => p.BusinessState, bs =>
        {
            bs.Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnName("Status")
                .IsRequired();
            
            bs.Property(b => b.IsAvailableForAdoption)
                .HasColumnName("IsAvailableForAdoption")
                .IsRequired();
            
            bs.Property(b => b.RescuedDate)
                .HasColumnName("RescuedDate")
                .HasColumnType("datetime2")
                .IsRequired();
            
            bs.Property(b => b.AdoptedDate)
                .HasColumnName("AdoptedDate")
                .HasColumnType("datetime2")
                .IsRequired(false);
        });
        
        builder.OwnsOne(p => p.PhysicalCharacteristics, pc =>
        {
            pc.Property(c => c.IsNeutered)
                .HasColumnName("IsNeutered")
                .IsRequired();
            
            pc.Property(c => c.IsVaccinated)
                .HasColumnName("IsVaccinated")
                .IsRequired();
            
            pc.Property(c => c.Weight)
                .HasColumnName("Weight")
                .HasColumnType("decimal(5,2)")
                .IsRequired();
            
            pc.Property(c => c.Height)
                .HasColumnName("Height")
                .HasColumnType("decimal(5,2)")
                .IsRequired();
            
        });
        
        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();
        
        builder.Property(p => p.UpdatedAt)
            .HasColumnType("datetime2")
            .IsRequired();
        
        builder.Ignore(p => p.DomainEvents);
    }
}