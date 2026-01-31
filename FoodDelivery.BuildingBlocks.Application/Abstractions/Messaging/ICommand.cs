using FoodDelivery.BuildingBlocks.Domain.Result;
using MediatR;
namespace FoodDelivery.BuildingBlocks.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface  IBaseCommand
{
}