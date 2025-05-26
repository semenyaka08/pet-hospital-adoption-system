namespace PHA.Domain.ValueObjects;

public record Name : IStrongType<string, Name>
{
    public string Value { get; }
    
    private Name(string value) => Value = value;
    
    public Name Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        return new Name(value);
    }
}