using Monolith.Core.Application.Abstractions.Queries;

namespace Monolith.Core.Application.Abstractions.Dispatcher;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}