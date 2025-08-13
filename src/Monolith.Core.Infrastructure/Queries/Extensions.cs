using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Application.Abstractions.Dispatcher;
using Monolith.Core.Application.Abstractions.Queries;
using Monolith.Core.Infrastructure.Attributes;
using Monolith.Core.Infrastructure.Queries.Decorators;

namespace Monolith.Core.Infrastructure.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueryHandling(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)).WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddPagedQueryDecorator(this IServiceCollection services)
    {
        services.TryDecorate(typeof(IQueryHandler<,>), typeof(PagedQueryHandlerDecorator<,>));
            
        return services;
    }
}