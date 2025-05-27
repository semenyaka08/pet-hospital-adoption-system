using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pet.Domain.Abstractions;

namespace Pet.Infrastructure.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntitiesState(eventData.Context);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntitiesState(DbContext? context)
    {
        if(context == null)
            return;
        
        var entries = context.ChangeTracker.Entries<IEntity>()
            .Where(e => e.State is EntityState.Modified or EntityState.Added);
        
        entries.ToList().ForEach(e => e.Entity.UpdatedAt = DateTime.UtcNow);
    }
}