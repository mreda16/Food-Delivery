using FoodDelivery.BuildingBlocks.Application.Abstractions.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.BuildingBlocks.Infrastructure.EventBus
{
    public sealed class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(
            TEvent @event,
            CancellationToken cancellationToken = default)
            where TEvent : class
        {
            using var scope = _serviceProvider.CreateScope();

            var handlers = scope.ServiceProvider
                .GetServices<IIntegrationEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.Handle(@event, cancellationToken);
            }
        }
    }
}
