namespace PHA.Domain.ValueObjects;

public record PhysicalCharacteristics
{
    public decimal Weight { get; set; }
    
    public decimal Height { get; set; }

    public bool IsNeutered { get; set; }

    public bool IsVaccinated { get; set; }
}