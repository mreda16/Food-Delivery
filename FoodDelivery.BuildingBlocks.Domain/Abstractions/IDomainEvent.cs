using System;

namespace FoodDelivery.BuildingBlocks.Domain.Abstractions
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredOnUtc { get; }
    }
}