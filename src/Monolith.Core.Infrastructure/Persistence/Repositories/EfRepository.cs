using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Monolith.Core.Application.Abstractions.Repositories;
using Monolith.Core.Shared.Models;
using Monolith.Core.Shared.Results;
using OneOf;
using Serilog;

namespace Monolith.Core.Infrastructure.Repositories;

public class EfRepository<T> : ISqlRepository<T> where T : class
    {
    private readonly DbSet<T> _collection;
    private readonly ILogger _logger;

    protected EfRepository(DbContext dbContext, ILogger logger)
    {
        _collection = dbContext.Set<T>();
        _logger = logger;
    }

    public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> conditionExpression = null) =>
        conditionExpression is null ? _collection : _collection.Where(conditionExpression);

    public IQueryable<T> GetQueryableFromRawQuery(string sql, params object[] parameters) =>
        sql is null ? _collection : _collection.FromSqlRaw(sql, parameters);

    public virtual Task<T> GetFirstByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        Func<IQueryable<T>, IQueryable<T>> specialAction = null,
        CancellationToken token = default)
    {
        var dataWithSpecialAction = specialAction?.Invoke(_collection) ?? _collection;
        return conditionExpression is null
            ? dataWithSpecialAction.FirstOrDefaultAsync(token)
            : dataWithSpecialAction.FirstOrDefaultAsync(conditionExpression, token);
    }

    public virtual Task<bool> ExistByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        CancellationToken token = default)
    {
        return conditionExpression is null
            ? _collection.AsNoTracking().AnyAsync(token)
            : _collection.AsNoTracking().AnyAsync(conditionExpression, token);
    }

    public virtual Task<List<T>> GetManyByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default)
    {
        var preFilter = _collection.Where(conditionExpression ?? (_ => true));
        var dataWithSpecialAction = specialAction?.Invoke(preFilter) ?? preFilter;
        return dataWithSpecialAction.ToListAsync(token);
    }

    public virtual async Task<Pagination<T>> GetManyByConditionWithPaginationAsync(
        Expression<Func<T, bool>> conditionExpression = null, Func<IQueryable<T>, IQueryable<T>> specialAction = null,
        CancellationToken token = default)
    {
        var items = await GetManyByConditionAsync(conditionExpression, specialAction, token);
        var totalRecord = await CountByConditionAsync(conditionExpression, specialAction, token);
        return new Pagination<T> { Items = items, TotalRecord = totalRecord };
    }

    public virtual Task<long> CountByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default)
    {
        var preFilter = _collection.Where(conditionExpression ?? (_ => true));
        var dataWithSpecialAction = specialAction?.Invoke(preFilter) ?? preFilter;
        return dataWithSpecialAction.LongCountAsync(token);
    }


    public async Task<T?> CreateOneAsync(T item, CancellationToken token = default)
    {
        if (item == null) return null;
        await _collection.AddAsync(item, token);
        
        return item;
    }

    public async Task<bool> CreateManyAsync(List<T> items, CancellationToken token = default)
    {
        if (items == null || !items.Any()) return false;
        await _collection.AddRangeAsync(items, token);
        return true;
    }

    public async Task<bool> RemoveOneAsync(Expression<Func<T, bool>> filter, CancellationToken token = default)
    {
        var entity = await _collection.FirstOrDefaultAsync(filter, token);
        if (entity == null) return false;

        _collection.Remove(entity);
        return true;
    }

    public async Task<bool> RemoveManyAsync(Expression<Func<T, bool>> filter, CancellationToken token = default)
    {
        var entities = await _collection.Where(filter).ToListAsync(token);
        if (!entities.Any()) return false;

        _collection.RemoveRange(entities);
        return true ;
    }
}