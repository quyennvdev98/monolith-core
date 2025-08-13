
using Monolith.Core.Application.Abstractions.Commands;

namespace Monolith.Core.Application.Abstractions.Dispatcher;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand;
}