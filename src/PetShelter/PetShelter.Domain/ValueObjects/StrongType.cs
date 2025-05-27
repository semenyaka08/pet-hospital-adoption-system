namespace PetShelter.Domain.ValueObjects;

public interface IStrongType<T, out TResult>
{
    public T Value { get;}
    
    static abstract TResult Of(T value);
}