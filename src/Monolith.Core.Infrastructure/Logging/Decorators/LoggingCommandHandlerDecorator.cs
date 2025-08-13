
using Humanizer;
using Microsoft.Extensions.Logging;
using Monolith.Core.Application.Abstractions.Commands;
using Monolith.Core.Infrastructure.Attributes;
using Monolith.Core.Shared.Contexts;

namespace DistributedSystem.BuildingBlock.Infrastructure.Logging.Decorators;

[Decorator]
public sealed class LoggingCommandHandlerDecorator<T> : ICommandHandler<T>
    where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IContext _context;
    private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<T> handler, 
        IContext context, ILogger<LoggingCommandHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleAsync(T command, CancellationToken cancellationToken)
    {
        
        var name = command.GetType().Name.Underscore();
        var requestId = _context.RequestId;
        var traceId = _context.TraceId;
        var userId = _context.Identity?.Id;
    
        await _handler.HandleAsync(command, cancellationToken);
        
    }
}