namespace PetShelter.Domain.ValueObjects;

public record Species : IStrongType<string, Species>
{
    public string Value { get; }
    
    private Species(string value) => Value = value;
    
    public static Species Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        return new Species(value);
    }
}