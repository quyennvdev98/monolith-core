
using Monolith.Core.Application.Exceptions;

namespace Monolith.Core.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
      ExceptionResponse Map(Exception exception);
}
