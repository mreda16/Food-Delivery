namespace FoodDelivery.BuildingBlocks.Application.Abstractions.EventBus
{
    public interface IIntegrationEventHandler<in TEvent>
        where TEvent : class
    {
        Task Handle(
            TEvent @event,
            CancellationToken cancellationToken = default);
    }
}
