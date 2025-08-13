
using System.Windows.Input;
using Monolith.Core.Application.Abstractions.Queries;

namespace Monolith.Core.Application.Abstractions.Dispatcher;
public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    // Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}