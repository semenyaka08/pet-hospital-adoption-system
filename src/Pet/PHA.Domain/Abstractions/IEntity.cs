namespace PHA.Domain.Abstractions;

public interface IEntity
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}