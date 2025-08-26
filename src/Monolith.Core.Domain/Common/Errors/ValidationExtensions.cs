

using FluentResults;
using FluentValidation;
using FluentValidation.Results;

namespace Monolith.Core.Domain.Common.Errors;

public static class ValidationExtensions
{
    public static Result<T> ToResult<T>(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Ok<T>(default(T)!);

        var errors = validationResult.Errors
            .Select(failure => DomainError.Validation(
                $"Validation.{failure.PropertyName}",
                failure.ErrorMessage,
                failure.PropertyName)
                .WithMetadata("AttemptedValue", failure.AttemptedValue)
                .WithMetadata("Severity", failure.Severity.ToString()));
            
        return Result.Fail<T>(errors);
    }

    public static Result ToResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Ok();

        var errors = validationResult.Errors
            .Select(failure => DomainError.Validation(
                $"Validation.{failure.PropertyName}",
                failure.ErrorMessage,
                failure.PropertyName)
                .WithMetadata("AttemptedValue", failure.AttemptedValue)
                .WithMetadata("Severity", failure.Severity.ToString()));
           

        return Result.Fail(errors);
    }

    public static async Task<Result<T>> ValidateAndExecuteAsync<T, TRequest>(
        this IValidator<TRequest> validator,
        TRequest request,
        Func<TRequest, Task<Result<T>>> action,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToResult<T>();

        return await action(request);
    }
}