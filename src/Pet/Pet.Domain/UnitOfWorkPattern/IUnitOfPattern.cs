namespace Pet.Domain.UnitOfWorkPattern;

public interface IUnitOfPattern
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}