namespace Monolith.Core.Application.Exceptions;

public abstract class SystemCoreException : Exception
{
    protected SystemCoreException(string message) : base(message)
    {
    }
}