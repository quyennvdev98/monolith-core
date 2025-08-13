using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Monolith.Core.Infrastructure.Exceptions;

public sealed class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionCompositionRoot _exceptionCompositionRoot;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot, ILogger<ErrorHandlerMiddleware> logger)
    {
        _exceptionCompositionRoot = exceptionCompositionRoot;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = _exceptionCompositionRoot.Map(exception);
        
        context.Response.StatusCode = (int) (errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        context.Response.ContentType = "application/json";
        var response = errorResponse?.Response;

        if (response is null)
        {
            return;
        }           
        await context.Response.WriteAsJsonAsync(response);
    }
}