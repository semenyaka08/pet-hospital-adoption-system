using Pet.Domain.DomainExceptions;

namespace Pet.Domain.ValueObjects;

public record DateOfBirth : IStrongType<DateTime, DateOfBirth>
{
    public DateTime Value { get; }
    
    private DateOfBirth(DateTime value) => Value = value;
    
    public DateOfBirth Of(DateTime value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value > DateTime.Now)
        {
            throw new DomainException("Date of birth cannot be in the future.");
        }
        
        if (value < new DateTime(1970, 1, 1))
        {
            throw new DomainException("Date of birth cannot be before January 1, 1900.");
        }
        
        return new DateOfBirth(value);
    }
}