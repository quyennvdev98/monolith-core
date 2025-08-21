using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Application.Abstractions.Commands;
using Monolith.Core.Application.Abstractions.Dispatcher;
using Monolith.Core.Infrastructure.Attributes;
using Monolith.Core.Infrastructure.Commands;

namespace Monolith.Core.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCommandHandling(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)).WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

}