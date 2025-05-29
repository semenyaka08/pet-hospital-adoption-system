using Hospital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Infrastructure;

public class HospitalDbContext(DbContextOptions<HospitalDbContext> contextOptions) : DbContext(options: contextOptions)
{
    public DbSet<Doctor> Doctors { get; set; }
    
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
}