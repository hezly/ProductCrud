using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// An implementation of the CQRS pattern using Meditr.
/// Command Handler interface for implementing CQRS pattern command (create, update, delete)
/// </summary>
public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
