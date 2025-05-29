using Hospital.API.Infrastructure;
using Hospital.API.Models;

namespace Hospital.API.DataSeeders;

public class DataSeeder(HospitalDbContext context) : IDataSeeder
{
    public async Task Seed()
    {
        await SeedDoctors();
    }
    
    private async Task SeedDoctors()
    {
        if (!context.Doctors.Any())
        {
            var doctor1 = new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = "Danya Minezeri",
                Email = "minezeri034@gmail.com",
            };

            var doctor2 = new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                Email = "serhiy.semenyaka@gmail.com"
            };

            var doctor3 = new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                Email = "semenyaka.sergey08@gmail.com\n"
            };
            
            context.Doctors.AddRange(doctor1, doctor2, doctor3);
            await context.SaveChangesAsync();
        }
    }
}