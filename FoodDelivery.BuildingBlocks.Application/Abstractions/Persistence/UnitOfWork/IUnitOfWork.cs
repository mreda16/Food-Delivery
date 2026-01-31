namespace FoodDelivery.BuildingBlocks.Application.Abstractions.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    // Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
