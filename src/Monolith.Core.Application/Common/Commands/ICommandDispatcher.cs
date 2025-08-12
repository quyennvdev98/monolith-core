namespace Monolith.Core.Application.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand;
}