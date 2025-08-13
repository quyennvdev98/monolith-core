using Monolith.Core.Shared.Results;
using OneOf;

namespace Monolith.Core.Shared.Abstractions;

public interface IUnitOfWork
{
    Task<OneOf<None, Exception>> SaveChangesAsync(CancellationToken token = default);
}
