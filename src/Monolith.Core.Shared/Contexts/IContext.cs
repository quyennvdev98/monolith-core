

namespace Monolith.Core.Application.Common.Contexts;

public interface IContext
{
    Guid RequestId { get; }
    Guid CorrelationId { get; }
    string TraceId { get; }
    string IpAddress { get; }
    IIdentityContext Identity { get; }
}