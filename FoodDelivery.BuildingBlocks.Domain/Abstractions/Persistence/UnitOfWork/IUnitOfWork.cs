namespace FoodDelivery.BuildingBlocks.Domain.Abstractions.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}