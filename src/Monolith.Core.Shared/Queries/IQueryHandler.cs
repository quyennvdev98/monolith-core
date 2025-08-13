namespace Monolith.Core.Shared.Queries;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}