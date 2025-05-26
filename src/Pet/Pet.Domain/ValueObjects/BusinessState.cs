using Pet.Domain.Enums;

namespace Pet.Domain.ValueObjects;

public record BusinessState
{
    public Status Status { get; } = Status.Rescued;

    public bool IsAvailableForAdoption { get; }

    public DateTime RescuedDate { get; set; }

    public DateTime? AdoptedDate { get; set; }

    private BusinessState(Status status, bool isAvailableForAdoption, DateTime rescuedDate)
    {
        Status = status;
        IsAvailableForAdoption = isAvailableForAdoption;
        RescuedDate = rescuedDate;
    }
    
    public static BusinessState Of(Status status, bool isAvailableForAdoption, DateTime rescuedDate, DateTime? adoptedDate = null)
    {
        ArgumentNullException.ThrowIfNull(status);
        ArgumentNullException.ThrowIfNull(isAvailableForAdoption);
        ArgumentNullException.ThrowIfNull(rescuedDate);
        
        if (rescuedDate > DateTime.UtcNow || rescuedDate < new DateTime(2000, 1, 1))
        {
            throw new ArgumentException("Rescued date cannot be in the future or before 2000.");
        }

        if(adoptedDate != null && (adoptedDate < rescuedDate || adoptedDate > DateTime.UtcNow))
        {
            throw new ArgumentException("Adopted date cannot be in the future or before the rescued date.");
        }
        
        return new BusinessState(status, isAvailableForAdoption, rescuedDate);
    }
}