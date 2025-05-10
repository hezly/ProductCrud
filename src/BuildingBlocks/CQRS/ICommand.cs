using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// An implementation of the CQRS pattern using Meditr.
/// Command interface for implementing CQRS pattern command (create, update, delete) 
/// and distinguishing it from query (get, get all)
/// </summary>
public interface ICommand : ICommand<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
