namespace Pet.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; }
}