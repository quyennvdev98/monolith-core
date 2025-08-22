using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


namespace Monolith.Core.Infrastructure.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("Swagger").Get<SwaggerOptions>();
        if (options?.Enabled != true)
            return services;

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(options.Name, new OpenApiInfo
            {
                Title = options.Title,
                Version = options.Version
            });

            c.ExampleFilters();

            AddSecurityDefinitions(c);

            var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        var options = configuration.GetSection("Swagger").Get<SwaggerOptions>();
        if (options?.Enabled != true)
            return app;

        var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

        app.UseStaticFiles()
           .UseSwagger(c => c.RouteTemplate = $"{routePrefix}/{{documentName}}/swagger.json");

        return options.ReDocEnabled
            ? app.UseReDoc(c =>
            {
                c.RoutePrefix = routePrefix;
                c.SpecUrl = $"{options.Name}/swagger.json";
            })
            : app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                c.RoutePrefix = routePrefix;
            });
    }

    private static void AddSecurityDefinitions(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
    {
        // JWT Bearer
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                Example: 'Bearer {your token}'",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        });

        // API Key
        c.AddSecurityDefinition("XApiKey", new OpenApiSecurityScheme
        {
            Description = "XApiKey is required for some API",
            Type = SecuritySchemeType.ApiKey,
            Name = "XApiKey",
            In = ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });

        // Apply Security globally
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            },
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "XApiKey" }
                },
                Array.Empty<string>()
            }
        });
    }
}