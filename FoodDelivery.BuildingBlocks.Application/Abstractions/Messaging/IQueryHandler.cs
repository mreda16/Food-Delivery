using FoodDelivery.BuildingBlocks.Domain.Result;
using MediatR;
namespace FoodDelivery.BuildingBlocks.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}