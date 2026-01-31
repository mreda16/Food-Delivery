using FoodDelivery.BuildingBlocks.Domain.Result;
using MediatR;
namespace FoodDelivery.BuildingBlocks.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}