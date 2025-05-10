using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// An implementation of the CQRS pattern using Meditr.
/// Query Handler interface for implementing CQRS pattern query (get, get all)
/// </summary>
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
