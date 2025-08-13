using Monolith.Core.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Monolith.Core.Infrastructure.Exceptions;

public sealed class ExceptionCompositionRoot : IExceptionCompositionRoot
{
    private readonly IServiceProvider _services;

    public ExceptionCompositionRoot(IServiceProvider services)
    {
        _services = services;
    }

    public ExceptionResponse Map(Exception exception)
    {
        using var scope = _services.CreateScope();

        var mappers = scope.ServiceProvider.GetServices<IExceptionToResponseMapper>().ToArray();

        var nonDefaultMappers = mappers.Where(x => x is not ExceptionToResponseMapper);
        var result = nonDefaultMappers
           .Select(x => x.Map(exception))
           .SingleOrDefault(x => x is not null);
            
        if (result is not null)
        {
            return result;
        }

        var defaultMapper = mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);

        return defaultMapper?.Map(exception);
    }
}