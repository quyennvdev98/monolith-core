


using FluentResults;

namespace Monolith.Core.Domain.Common.Errors;

public static class ResultExtensions
{
    public static Result<TDestination> Map<TSource, TDestination>(
        this Result<TSource> result, 
        Func<TSource, TDestination> mapper)
    {
        return result.IsSuccess 
            ? Result.Ok(mapper(result.Value))
            : Result.Fail<TDestination>(result.Errors);
    }

    public static async Task<Result<TDestination>> MapAsync<TSource, TDestination>(
        this Result<TSource> result, 
        Func<TSource, Task<TDestination>> mapper)
    {
        if (result.IsSuccess)
        {
            var mappedValue = await mapper(result.Value);
            return Result.Ok(mappedValue);
        }
        
        return Result.Fail<TDestination>(result.Errors);
    }

    public static Result<TDestination> Bind<TSource, TDestination>(
        this Result<TSource> result, 
        Func<TSource, Result<TDestination>> binder)
    {
        return result.IsSuccess 
            ? binder(result.Value)
            : Result.Fail<TDestination>(result.Errors);
    }

    public static async Task<Result<TDestination>> BindAsync<TSource, TDestination>(
        this Result<TSource> result, 
        Func<TSource, Task<Result<TDestination>>> binder)
    {
        return result.IsSuccess 
            ? await binder(result.Value)
            : Result.Fail<TDestination>(result.Errors);
    }

    public static Result<T> Ensure<T>(
        this Result<T> result, 
        Func<T, bool> predicate, 
        DomainError error)
    {
        if (result.IsFailed)
            return result; 

        return predicate(result.Value) 
            ? result 
            : Result.Fail<T>(error);
    }

    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);
        
        return result;
    }

    public static Result<T> OnFailure<T>(this Result<T> result, Action<IEnumerable<IError>> action)
    {
        if (result.IsFailed)
            action((IEnumerable<IError>)result.Errors);
        
        return result;
    }

    public static bool HasErrorOfType(this ResultBase result, ErrorType errorType)
    {
        return result.Errors.Any(e => 
            e.Metadata.ContainsKey("Type") && 
            e.Metadata["Type"].ToString() == errorType.ToString());
    }

    public static string GetFirstErrorMessage(this ResultBase result)
    {
        return result.Errors.FirstOrDefault()?.Message ?? "Unknown error occurred";
    }
}