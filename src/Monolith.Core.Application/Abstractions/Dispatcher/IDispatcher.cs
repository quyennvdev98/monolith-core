
using Monolith.Core.Application.Abstractions.Commands;
using Monolith.Core.Application.Abstractions.Queries;

namespace Monolith.Core.Application.Abstractions.Dispatcher;
public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}