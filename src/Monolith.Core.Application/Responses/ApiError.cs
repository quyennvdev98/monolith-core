namespace Monolith.Core.Application.Responses;

public class ApiError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Property { get; set; }

    public ApiError() { }

    public ApiError(string code, string message, string? property = null)
    {
        Code = code;
        Message = message;
        Property = property;
    }
}