namespace PetShelter.Domain.ValueObjects;

public record PhysicalCharacteristics
{
    public decimal Weight { get; set; }
    
    public decimal Height { get; set; }

    public bool IsNeutered { get; set; }

    public bool IsVaccinated { get; set; }

    private PhysicalCharacteristics(decimal weight, decimal height, bool isNeutered, bool isVaccinated)
    {
        Weight = weight;
        Height = height;
        IsNeutered = isNeutered;
        IsVaccinated = isVaccinated;
    }

    public static PhysicalCharacteristics Of(decimal weight, decimal height, bool isNeutered, bool isVaccinated)
    {
        ArgumentNullException.ThrowIfNull(weight);
        ArgumentNullException.ThrowIfNull(height);
        ArgumentNullException.ThrowIfNull(isNeutered);
        ArgumentNullException.ThrowIfNull(isVaccinated);
        
        return new PhysicalCharacteristics(weight, height, isNeutered, isVaccinated);
    }
}