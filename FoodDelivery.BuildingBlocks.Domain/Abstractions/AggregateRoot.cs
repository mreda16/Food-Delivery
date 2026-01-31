namespace FoodDelivery.BuildingBlocks.Domain.Abstractions
{
    public abstract class AggregateRoot<TKey, TAggregateRoot>
        : Entity<TKey, TAggregateRoot>
        where TAggregateRoot : AggregateRoot<TKey, TAggregateRoot>
    {
        protected AggregateRoot()
        {
        }
    }
}