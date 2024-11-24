using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

/// <summary>
/// Class for building asynchronous queries on a data source using a fluent approach.
/// </summary>
/// <typeparam name="T">Entity type in the database context.</typeparam>
public class AsyncDbSearchBuilder<T>(DbSet<T> set) where T : class
{
    private IAsyncEnumerable<T> _asyncEnumerable = set.ToAsyncEnumerable();
    
    /// <summary>
    /// Applies a filter to the query.
    /// </summary>
    /// <param name="predicate">The filter condition.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    public AsyncDbSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        
        _asyncEnumerable = _asyncEnumerable.Where(predicate);
        return this;
    }
    
    
    /// <summary>
    /// Applies pagination to the query.
    /// </summary>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="elements">The number of elements per page.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    public AsyncDbSearchBuilder<T> Page(int page,
        int elements)
    {
        var countElements = (page - 1) * elements;
        _asyncEnumerable = _asyncEnumerable.Skip(countElements);
        return Elements(elements);
    }

    /// <summary>
    /// Limits the number of elements returned by the query.
    /// </summary>
    /// <param name="elements">The number of elements to return.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    public AsyncDbSearchBuilder<T> Elements(int elements)
    {
        _asyncEnumerable = _asyncEnumerable.Take(elements);
        return this;
    }
    /// <summary>
    /// Orders the query by the specified predicate in ascending or descending order.
    /// </summary>
    /// <param name="predicate">The predicate to order by.</param>
    /// <param name="ascending">True for ascending order, false for descending.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    public AsyncDbSearchBuilder<T> OrderBy(Func<T, object> predicate, bool ascending)
    {
        return ascending ? OrderByAscending(predicate) : OrderByDescending(predicate);
    }
    
    /// <summary>
    /// Orders the query by the specified predicate in ascending order.
    /// </summary>
    /// <param name="predicate">The predicate to order by.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    private AsyncDbSearchBuilder<T> OrderByDescending(Func<T, object> predicate)
    {
        _asyncEnumerable = _asyncEnumerable.OrderByDescending(predicate);
        return this;
    }
    /// <summary>
    /// Orders the query by the specified predicate in descending order.
    /// </summary>
    /// <param name="predicate">The predicate to order by.</param>
    /// <returns>Returns the builder to allow method chaining.</returns>
    private AsyncDbSearchBuilder<T> OrderByAscending(Func<T, object> predicate)
    {
        _asyncEnumerable = _asyncEnumerable.OrderBy(predicate);
        return this;
    }
    
    /// <summary>
    /// Executes the query and retrieves all results as a list.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, with the list of results.</returns>
    public async Task<List<T>> BuildAndGetAll()
    {
        return await _asyncEnumerable
            .ToListAsync();
    }

    /// <summary>
    /// Executes the query and retrieves the first result.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, with the first result.</returns>
    public async Task<T?> BuildAndGetFirst()
    {
        return await _asyncEnumerable.FirstAsync();
    }

    /// <summary>
    /// Creates a new instance of the builder for the specified DbSet.
    /// </summary>
    /// <param name="set">The DbSet to query against.</param>
    /// <returns>A new instance of AsyncDbSearchBuilder.</returns>
    public static AsyncDbSearchBuilder<T> NewBuilder(DbSet<T> set)
    {
        return new AsyncDbSearchBuilder<T>(set);
    }

    
    
}