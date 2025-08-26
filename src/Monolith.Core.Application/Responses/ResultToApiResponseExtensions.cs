

using FluentResults;
using Microsoft.AspNetCore.Http;
using Monolith.Core.Domain.Common.Errors;

namespace Monolith.Core.Application.Responses;

public static class ResultToApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result, string? successMessage = null)
    {
        if (result.IsSuccess)
        {
            return ApiResponse<T>.SuccessResult(result.Value, successMessage);
        }

        var errors = result.Errors.Select(MapToApiError).ToList();
        var mainErrorMessage = GetMainErrorMessage(result.Errors);

        return ApiResponse<T>.FailureResult(mainErrorMessage, errors);
    }

    public static ApiResponse ToApiResponse(this Result result, string? successMessage = null)
    {
        if (result.IsSuccess)
        {
            return ApiResponse.SuccessResult(successMessage);
        }

        var errors = result.Errors.Select(MapToApiError).ToList();
        var mainErrorMessage = GetMainErrorMessage(result.Errors);

        return ApiResponse.FailureResult(mainErrorMessage, errors);
    }

    public static (ApiResponse<T> response, int statusCode) ToApiResponseWithStatusCode<T>(this Result<T> result, string? successMessage = null)
    {
        var response = result.ToApiResponse(successMessage);
        var statusCode = GetStatusCode(result);
        
        return (response, statusCode);
    }

    public static (ApiResponse response, int statusCode) ToApiResponseWithStatusCode(this Result result, string? successMessage = null)
    {
        var response = result.ToApiResponse(successMessage);
        var statusCode = GetStatusCode(result);
        
        return (response, statusCode);
    }

    private static ApiError MapToApiError(IError error)
    {
        var code = error.Metadata.ContainsKey("Code") 
            ? error.Metadata["Code"].ToString()! 
            : "UnknownError";
            
        var property = error.Metadata.ContainsKey("Property") 
            ? error.Metadata["Property"]?.ToString() 
            : null;

        return new ApiError(code, error.Message, property);
    }

    private static string GetMainErrorMessage(IEnumerable<IError> errors)
    {
        var errorTypes = errors
            .Where(e => e.Metadata.ContainsKey("Type"))
            .Select(e => e.Metadata["Type"].ToString())
            .Distinct()
            .ToList();

        if (errorTypes.Contains(ErrorType.Validation.ToString()))
            return "Validation failed. Please check your input.";
        
        if (errorTypes.Contains(ErrorType.NotFound.ToString()))
            return "The requested resource was not found.";
        
        if (errorTypes.Contains(ErrorType.Conflict.ToString()))
            return "A conflict occurred with the current state of the resource.";
        
        if (errorTypes.Contains(ErrorType.Unauthorized.ToString()))
            return "You are not authorized to perform this action.";
        
        if (errorTypes.Contains(ErrorType.Forbidden.ToString()))
            return "Access to this resource is forbidden.";

        return "An error occurred while processing your request.";
    }

    private static int GetStatusCode(ResultBase result)
    {
        if (result.IsSuccess)
            return StatusCodes.Status200OK;

        var errorTypes = result.Errors
            .Where(e => e.Metadata.ContainsKey("Type"))
            .Select(e => e.Metadata["Type"].ToString())
            .Distinct()
            .ToList();

        if (errorTypes.Contains(ErrorType.Unauthorized.ToString()))
            return StatusCodes.Status401Unauthorized;
        
        if (errorTypes.Contains(ErrorType.Forbidden.ToString()))
            return StatusCodes.Status403Forbidden;
        
        if (errorTypes.Contains(ErrorType.NotFound.ToString()))
            return StatusCodes.Status404NotFound;
        
        if (errorTypes.Contains(ErrorType.Conflict.ToString()))
            return StatusCodes.Status409Conflict;
        
        if (errorTypes.Contains(ErrorType.Validation.ToString()))
            return StatusCodes.Status400BadRequest;

        return StatusCodes.Status500InternalServerError;
    }
}