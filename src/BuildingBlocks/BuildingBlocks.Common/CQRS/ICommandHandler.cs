using MediatR;

namespace BuildingBlocks.Common.CQRS;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : notnull;

public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : IRequest;