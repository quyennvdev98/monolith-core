using System.Net;

namespace  Monolith.Core.Application.Exceptions;
public record ExceptionResponse(object Response, HttpStatusCode StatusCode);