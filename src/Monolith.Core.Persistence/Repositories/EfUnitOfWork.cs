
using Microsoft.EntityFrameworkCore;
using Monolith.Core.Shared.Abstractions;
using Monolith.Core.Shared.Results;
using OneOf;
using Serilog;

namespace Monolith.Core.Infrastructure.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private readonly ILogger _logger;

    protected EfUnitOfWork(DbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<OneOf<None, Exception>> SaveChangesAsync(CancellationToken token = default)
    {
        _logger.Information("Save context async!");
        try
        {
            await _dbContext.SaveChangesAsync(token);
            return None.Value;
        }
        catch (Exception e)
        {
            _logger.Error("Error while save changes using dbContext: {Error}", e.Message);
            return e;
        }
    }
}