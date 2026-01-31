namespace FoodDelivery.BuildingBlocks.Domain.Abstractions;

// Non-generic base (of the aggregate) for infrastructure
public interface IAggregateRoot
{
    
}

/* the problem is we can not use it like that: .Entries<AggregateRoot>() cuz Aggregate root is generic so we needed to
   introduce a NON-generic marker base for aggregates
   
- we also can not make it Abstract class cuz Aggregate root inherits from Entity and Multiple inheritance is not allowed
*/