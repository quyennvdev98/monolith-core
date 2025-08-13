
using DistributedSystem.BuildingBlock.Infrastructure.Logging.Decorators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monolith.Core.Application.Abstractions.Commands;
using Monolith.Core.Application.Abstractions.Queries;
using Monolith.Core.Infrastructure.App;
using Monolith.Core.Infrastructure.Logging;
using Monolith.Core.Infrastructure.Logging.Decorators;
using Monolith.Core.Shared.Contexts;
using Serilog;
using Serilog.Events;
using Serilog.Filters;


namespace DistributedSystem.BuildingBlock.Infrastructure.Logging;

public static class Extensions
{

    private const string ConsoleOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
    private const string AppSectionName = "app";
    private const string LoggerSectionName = "logger";

    public static IServiceCollection AddLoggingDecorators(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingQueryHandlerDecorator<,>));

        return services;
    }

    public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
    {
        app.Use(async (ctx, next) =>
        {
            var logger = ctx.RequestServices.GetRequiredService<ILogger<IContext>>();
            var context = ctx.RequestServices.GetRequiredService<IContext>();
            logger.LogInformation("Started processing a request [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']...",
                context.RequestId, context.CorrelationId, context.TraceId, context.Identity.IsAuthenticated ? context.Identity.Id : string.Empty);

            await next();

            logger.LogInformation("Finished processing a request with status code: {StatusCode} [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']",
                ctx.Response.StatusCode, context.RequestId, context.CorrelationId, context.TraceId, context.Identity.IsAuthenticated ? context.Identity.Id : string.Empty);
        });

        return app;
    }


    public static IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggerConfiguration>? configure = null,
        string loggerSectionName = LoggerSectionName,
        string appSectionName = AppSectionName)
        => builder.UseSerilog((context, loggerConfiguration) =>
        {
            if (string.IsNullOrWhiteSpace(loggerSectionName))
            {
                loggerSectionName = LoggerSectionName;
            }

            if (string.IsNullOrWhiteSpace(appSectionName))
            {
                appSectionName = AppSectionName;
            }

            var appOptions = new AppOptions();
            context.Configuration.GetSection(appSectionName).Bind(appOptions);

            var loggerOptions = new LoggerOptions();
            context.Configuration.GetSection(loggerSectionName).Bind(loggerOptions);

            MapOptions(loggerOptions, appOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
            configure?.Invoke(loggerConfiguration);
        });
        

    private static void MapOptions(LoggerOptions loggerOptions, AppOptions appOptions,
        LoggerConfiguration loggerConfiguration, string environmentName)
    {
        var level = GetLogEventLevel(loggerOptions.Level);

        loggerConfiguration.Enrich.FromLogContext()
            .MinimumLevel.Is(level)
            .Enrich.WithProperty("Environment", environmentName)
            .Enrich.WithProperty("Application", appOptions.AppName)
            .Enrich.WithProperty("Instance", appOptions.InstanceId)
            .Enrich.WithProperty("Version", appOptions.Version);

        foreach (var (key, value) in loggerOptions.Tags ?? new Dictionary<string, object>())
        {
            loggerConfiguration.Enrich.WithProperty(key, value);
        }

        foreach (var (key, value) in loggerOptions.Overrides ?? new Dictionary<string, string>())
        {
            var logLevel = GetLogEventLevel(value);
            loggerConfiguration.MinimumLevel.Override(key, logLevel);
        }

        loggerOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

        loggerOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty(p)));

        Configure(loggerConfiguration, loggerOptions);
    }

    private static void Configure(LoggerConfiguration loggerConfiguration, LoggerOptions options)
    {
        var consoleOptions = options.Console ?? new ConsoleOptions();
       

        if (consoleOptions.Enabled)
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: ConsoleOutputTemplate);
        }
    }

    private static LogEventLevel GetLogEventLevel(string level)
        => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
            ? logLevel
            : LogEventLevel.Information;

}