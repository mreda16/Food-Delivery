namespace FoodDelivery.BuildingBlocks.Domain.Abstractions
{
    public abstract class AggregateRoot<TKey, TAggregate>
        : Entity<TKey, TAggregate>
        where TAggregate : AggregateRoot<TKey, TAggregate>
    {
        protected AggregateRoot()
        {
        }
    }
}