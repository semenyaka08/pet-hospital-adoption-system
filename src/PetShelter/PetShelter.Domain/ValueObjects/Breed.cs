namespace PetShelter.Domain.ValueObjects;

public record Breed : IStrongType<string, Breed>
{
    public string Value { get; }
    
    private Breed(string value) => Value = value;
    
    public static Breed Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new Breed(value);
    }
}