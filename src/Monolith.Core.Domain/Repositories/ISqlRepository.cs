using System.Linq.Expressions;

namespace Monolith.Core.Domain.Repositories;

public interface ISqlRepository<T> where T : class
{
    IQueryable<T> GetQueryable(Expression<Func<T, bool>> conditionExpression = null);
    IQueryable<T> GetQueryableFromRawQuery(string sql, params object[] parameters);

    Task<T> GetFirstByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default);

    Task<bool> ExistByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        CancellationToken token = default);

    Task<List<T>> GetManyByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
        Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default);

    // Task<Pagination<T>> GetManyByConditionWithPaginationAsync(Expression<Func<T, bool>> conditionExpression = null,
    //     Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default);

    Task<long> CountByConditionAsync(Expression<Func<T, bool>> conditionExpression = null,
    Func<IQueryable<T>, IQueryable<T>> specialAction = null, CancellationToken token = default);

    Task<T> CreateOneAsync(T item, CancellationToken token = default);

    Task CreateManyAsync(List<T> items, CancellationToken token = default);

    Task RemoveOneAsync(Expression<Func<T, bool>> itemOrFilter,
        CancellationToken token = default);

    // Task RemoveManyAsync(List<T>,Expression<Func<T, bool>> itemsOrFilter,
    //     CancellationToken token = default);

}

