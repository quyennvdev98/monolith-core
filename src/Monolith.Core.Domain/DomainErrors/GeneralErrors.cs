using Monolith.Core.Domain.Common.Errors;

namespace Monolith.Core.Domain.DomainErrors;

public static class GeneralErrors
{
    public static DomainError DatabaseError(string operation, string details) =>
            DomainError.Internal(
                "Database.OperationFailed", 
                $"Database operation '{operation}' failed: {details}")
                .WithMetadata("Operation", operation)
                .WithMetadata("Details", details);

        public static DomainError UnexpectedError(Exception exception) =>
            DomainError.Internal(
                "General.UnexpectedError", 
                "An unexpected error occurred")
                .WithMetadata("Exception", exception.GetType().Name)
                .WithMetadata("ExceptionMessage", exception.Message)
                .WithMetadata("StackTrace", exception.StackTrace);

        public static DomainError ValidationFailed(string details) =>
            DomainError.Validation(
                "General.ValidationFailed", 
                $"Validation failed: {details}", 
                "Multiple");
}