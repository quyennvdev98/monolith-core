

namespace Monolith.Core.Application.Exceptions;

public interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}
    