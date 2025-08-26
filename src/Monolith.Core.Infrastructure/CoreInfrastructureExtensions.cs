using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Infrastructure.App;
using Monolith.Core.Infrastructure.Exceptions;
using Monolith.Core.Infrastructure.Queries;
using Monolith.Core.Infrastructure.Serialization;


namespace Monolith.Core.Infrastructure;

public static class CoreInfrastructureExtensions
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.AddApp(configuration);
        services.AddSerialization();
        services.AddLogging();
        services.AddPersistence(configuration);
        services.AddCommandHandling(assemblies);
        services.AddQueryHandling(assemblies);
        
        return services;
    }
}