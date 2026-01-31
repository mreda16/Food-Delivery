using FoodDelivery.BuildingBlocks.Application.Abstractions.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.BuildingBlocks.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
