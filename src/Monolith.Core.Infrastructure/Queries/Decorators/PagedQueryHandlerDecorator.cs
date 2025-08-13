
using Monolith.Core.Application.Abstractions.Queries;
using Monolith.Core.Infrastructure.Attributes;

namespace Monolith.Core.Infrastructure.Queries.Decorators;

[Decorator]
public sealed class PagedQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _innerHandler;

    public PagedQueryHandlerDecorator(IQueryHandler<TQuery, TResult> innerHandler)
    {
        _innerHandler = innerHandler;
    }

    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        const int maxResults = 100; 
        const int defaultResults = 10;
       if (query is IPagedQuery pagedQuery)
        {
            if (pagedQuery.Page <= 0)
            {
                pagedQuery.Page = 1;
            }

            if (pagedQuery.Results <= 0)
            {
                pagedQuery.Results = defaultResults;
            }

            if (pagedQuery.Results > maxResults)
            {
                pagedQuery.Results = maxResults;
            }
        }
        return await _innerHandler.HandleAsync(query, cancellationToken);
    }
}