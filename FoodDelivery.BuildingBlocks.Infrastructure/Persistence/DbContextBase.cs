using FoodDelivery.BuildingBlocks.Domain;
using FoodDelivery.BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.BuildingBlocks.Infrastructure.Persistence;

public abstract class DbContextBase<TKey, TAggregateRoot> : DbContext where TAggregateRoot : AggregateRoot<TKey, TAggregateRoot>
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    protected DbContextBase(
        DbContextOptions options,
        IDomainEventDispatcher domainEventDispatcher)
        : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        // 1. Collect domain events
        var domainEvents = ChangeTracker
            .Entries<AggregateRoot<TKey, TAggregateRoot>>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        // 2. Save changes
        var result = await base.SaveChangesAsync(cancellationToken);

        // 3. Dispatch domain events
        await _domainEventDispatcher.DispatchAsync(domainEvents);

        // 4. Clear domain events
        foreach (var entity in ChangeTracker.Entries<AggregateRoot<TKey, TAggregateRoot>>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}
