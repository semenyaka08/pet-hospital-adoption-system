using Pet.Domain.DomainExceptions;

namespace Pet.Domain.ValueObjects;

public record PetId : IStrongType<Guid, PetId>
{
    public Guid Value { get; }

    private PetId(Guid value)
    {
        Value = value;
    }

    public static PetId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        if (value == Guid.Empty)
        {
            throw new DomainException("PetId cannot be an empty GUID.");
        }
        
        return new PetId(value);
    }
}