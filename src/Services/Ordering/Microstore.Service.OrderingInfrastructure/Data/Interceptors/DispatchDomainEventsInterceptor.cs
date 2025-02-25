using MediatR;

namespace Microstore.Service.OrderingInfrastructure.Data.Interceptors;
public class DispatchDomainEventsInterceptor
    (IMediator mediator)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges
    (
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
    (
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null) return;

        IEnumerable<IAggregate> aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity);

        List<IDomainEvent> domainEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();

        aggregates.ToList().ForEach(x => x.ClearDomainEvents());

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
