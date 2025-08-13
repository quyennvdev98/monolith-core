

using Monolith.Core.Application.Abstractions.Commands;
using Monolith.Core.Application.Abstractions.Dispatcher;
using Monolith.Core.Application.Abstractions.Queries;

namespace Monolith.Core.Infrastructure.Dispatchers;

public sealed class InMemoryDispatcher : IDispatcher
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        => _queryDispatcher.QueryAsync(query, cancellationToken);

    public Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : class, ICommand
        => _commandDispatcher.SendAsync(command, cancellationToken);    

   
}