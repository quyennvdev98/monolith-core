using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Monolith.Core.Infrastructure.App;

public static class Extensions
{
    private const string SectionName = "AppCore";
    
    public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration configuration, string sectionName = SectionName)
    {
        var section = configuration.GetSection(sectionName);
        services.Configure<AppOptions>(section);
        return services;
    }
}