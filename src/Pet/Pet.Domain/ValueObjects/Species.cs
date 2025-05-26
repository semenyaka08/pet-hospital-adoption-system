namespace Pet.Domain.ValueObjects;

public record Species : IStrongType<string, Species>
{
    public string Value { get; }
    
    private Species(string value) => Value = value;
    
    public Species Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        return new Species(value);
    }
}