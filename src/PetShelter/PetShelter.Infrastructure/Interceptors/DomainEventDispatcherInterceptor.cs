using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetShelter.Domain.Abstractions;

namespace PetShelter.Infrastructure.Interceptors;

public class DomainEventDispatcherInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        await DispatchDomainEvents(eventData.Context);
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null)
            return;

        var aggregates = context.ChangeTracker.Entries<IAggregate>()
            .Where(z=>z.Entity.DomainEvents.Any())
            .Select(z => z.Entity)
            .ToList();

        var domainEvents = aggregates.SelectMany(z => z.DomainEvents);
        
        aggregates.ForEach(z => z.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}