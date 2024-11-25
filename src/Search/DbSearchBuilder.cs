using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

/// <summary>
/// This class is used to build a query using the Builder design pattern.
/// It allows applying filters, pagination, ordering, and retrieval of the results.
/// </summary>
/// <typeparam name="T">The object type that is being retrieved from the DbSet.</typeparam>


public class DbSearchBuilder<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private IEnumerable<T> _enumerable;

    /// <summary>
    /// Initializes a new instance of the DbSearchBuilder class.
    /// </summary>
    /// <param name="dbSet">The DbSet<T> to build the query on.</param>
    public DbSearchBuilder(DbSet<T> dbSet)
    {
        
        _dbSet = dbSet;
        _enumerable = _dbSet;
    }

    /// <summary>
    /// Applies a filter to the query based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The filter condition to apply.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    public DbSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        _enumerable = _dbSet.Where(predicate);
        return this;
    }

    /// <summary>
    /// Applies pagination to the query, limiting the results to the specified page and number of elements per page.
    /// </summary>
    /// <param name="page">The page number to retrieve (1-based index).</param>
    /// <param name="elements">The number of elements to return per page.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    public DbSearchBuilder<T> Page(int page,
        int elements)
    {
        var countElements = (page - 1) * elements;
        _enumerable = _enumerable.Skip(countElements);
        return Elements(elements);
    }

    /// <summary>
    /// Limits the number of elements returned by the query.
    /// </summary>
    /// <param name="elements">The number of elements to return.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    public DbSearchBuilder<T> Elements(int elements)
    {
        _enumerable = _enumerable.Take(elements);
        return this;
    }

    /// <summary>
    /// Orders the results by the specified predicate in either ascending or descending order.
    /// </summary>
    /// <param name="predicate">The predicate to order the results by.</param>
    /// <param name="ascending">True for ascending order, false for descending order.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    public DbSearchBuilder<T> OrderBy(Func<T, object> predicate, bool ascending)
    {
        return ascending ? OrderByAscending(predicate) : OrderByDescending(predicate);
    }

    /// <summary>
    /// Orders the results by the specified predicate in ascending order.
    /// </summary>
    /// <param name="predicate">The predicate to order the results by.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    private DbSearchBuilder<T> OrderByDescending(Func<T, object> predicate)
    {
        _enumerable = _enumerable.OrderByDescending(predicate);
        return this;
    }
    
    /// <summary>
    /// Orders the results by the specified predicate in descending order.
    /// </summary>
    /// <param name="predicate">The predicate to order the results by.</param>
    /// <returns>The current builder instance, allowing for method chaining.</returns>
    private DbSearchBuilder<T> OrderByAscending(Func<T, object> predicate)
    {
        _enumerable = _enumerable.OrderBy(predicate);
        
        return this;
    }

    /// <summary>
    /// Executes the query and retrieves all results as a list.
    /// </summary>
    /// <returns>A list containing all the elements from the query.</returns>
    public List<T> BuildAndGetAll()
    {
        
    
        return _enumerable
            .ToList();
    }

    /// <summary>
    /// Executes the query and retrieves the first result.
    /// </summary>
    /// <returns>The first result from the query, or null if no result is found.</returns>
    public T? BuildAndGetFirst()
    {
        return _enumerable
            .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new instance of the DbSearchBuilder class.
    /// </summary>
    /// <param name="dbSet">The DbSet<T> to query against.</param>
    /// <returns>A new instance of DbSearchBuilder.</returns>
    public static DbSearchBuilder<T> NewBuilder(DbSet<T> dbSet)
    {
        return new DbSearchBuilder<T>(dbSet);
    }
    
}