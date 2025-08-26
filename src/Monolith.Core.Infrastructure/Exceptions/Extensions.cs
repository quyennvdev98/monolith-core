using Microsoft.AspNetCore.Builder;

namespace Monolith.Core.Infrastructure.Exceptions;

public static class Extensions
{
    

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

}