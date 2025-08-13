
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Monolith.Core.Application.Exceptions;

namespace Monolith.Core.Infrastructure.Exceptions;

public static class Extensions
{
    public static void AddExceptionHandling(this IServiceCollection services)
        => services.AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>()
            .AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();

}