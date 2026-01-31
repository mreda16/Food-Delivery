namespace FoodDelivery.BuildingBlocks.Domain.Abstractions;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents);
}