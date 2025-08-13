using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Persistence;

namespace Monolith.Core.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    private const string SectionName = "Postgres";

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)

    {
        services.AddPostgres<CoreDbContext>(configuration);
        return services;
    }

    private static IServiceCollection AddPostgres<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = SectionName) where TDbContext : DbContext
    {
        var dbConnectionString = configuration.GetValue<string>($"{SectionName}:ConnectionString");

        services.AddDbContext<TDbContext>(x => x.UseNpgsql(dbConnectionString,
            options => options.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name)));
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<TDbContext>());
        return services;
    }
     
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        

        return services;
    }
}