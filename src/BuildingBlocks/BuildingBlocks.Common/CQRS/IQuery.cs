using MediatR;

namespace BuildingBlocks.Common.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>;

public interface IQuery : IRequest;