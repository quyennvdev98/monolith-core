
using FluentResults;

namespace Monolith.Core.Domain.Common.Errors;

public class DomainError : IError
{
    public string Code { get; private set; }
    public string Message { get; private set; }
    public string? Property { get; private set; }
    public ErrorType Type { get; private set; }
    public Dictionary<string, object> Metadata { get; private set; }

    public List<IError> Reasons => new ();

    private DomainError(string code, string message, ErrorType type, string? property = null)
    {
        Code = code;
        Message = message;
        Type = type;
        Property = property;
        Metadata = new Dictionary<string, object>();
    }

    public static DomainError Validation(string code, string message, string property) =>
        new(code, message, ErrorType.Validation, property);

    public static DomainError NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static DomainError Conflict(string code, string message, string? property = null) =>
        new(code, message, ErrorType.Conflict, property);

    public static DomainError Unauthorized(string code, string message) =>
        new(code, message, ErrorType.Unauthorized);

    public static DomainError Forbidden(string code, string message) =>
        new(code, message, ErrorType.Forbidden);

    public static DomainError Internal(string code, string message) =>
        new(code, message, ErrorType.Internal);

    public static DomainError External(string code, string message) =>
        new(code, message, ErrorType.External);

    public DomainError WithMetadata(string key, object value)
    {
        Metadata[key] = value;
        return this;
    }

    public DomainError WithMetadata(Dictionary<string, object> metadata)
    {
        foreach (var item in metadata)
        {
            Metadata[item.Key] = item.Value;
        }
        return this;
    }

   
}

