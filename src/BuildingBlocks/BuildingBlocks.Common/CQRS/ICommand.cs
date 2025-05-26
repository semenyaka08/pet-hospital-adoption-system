using MediatR;

namespace BuildingBlocks.Common.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>;