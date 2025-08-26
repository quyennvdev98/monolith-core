namespace Monolith.Core.Domain.Common.Errors;
public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    Internal,
    External
}
