namespace PHA.Domain.ValueObjects;

public interface IStrongType<T, out TResult>
{
    public T Value { get;}
    
    TResult Of(T value);
}