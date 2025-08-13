using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Application.Abstractions;

namespace Monolith.Core.Infrastructure.Serialization;

public static class Extensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services.AddSingleton<ISerializer, JsonSerializer>();
        return services;
    }
}