namespace PHA.Domain.ValueObjects;

public record Breed : IStrongType<string, Breed>
{
    public string Value { get; }
    
    private Breed(string value) => Value = value;
    
    public Breed Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new Breed(value);
    }
}