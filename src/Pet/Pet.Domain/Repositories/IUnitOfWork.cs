namespace Pet.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}