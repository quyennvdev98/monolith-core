using Monolith.Core.Shared.Results;
using OneOf;

namespace Monolith.Core.Shared.Abstractions;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken token = default);
}
