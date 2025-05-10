using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// An implementation of the CQRS pattern using Meditr.
/// Query interface for implementing CQRS pattern query (get, get all)
/// </summary>
public interface IQuery : IQuery<Unit>
{
}
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
