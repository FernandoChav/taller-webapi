using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

/// <summary>
/// This class is used for create a query
/// this use a build design pattern
/// </summary>
/// <typeparam name="T">A Object retrieve</typeparam>

public class DbSearchBuilder<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private IEnumerable<T> _enumerable;

    public DbSearchBuilder(DbSet<T> dbSet)
    {
        
        _dbSet = dbSet;
        _enumerable = _dbSet;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    
    public DbSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        _enumerable = _dbSet.Where(predicate);
        return this;
    }

    public DbSearchBuilder<T> Page(int page,
        int elements)
    {
        var countElements = (page - 1) * elements;
        _enumerable = _enumerable.Skip(countElements);
        return Elements(elements);
    }

    public DbSearchBuilder<T> Elements(int elements)
    {
        _enumerable = _enumerable.Take(elements);
        return this;
    }

    public DbSearchBuilder<T> OrderBy(Func<T, object> predicate, bool ascending)
    {
        return ascending ? OrderByAscending(predicate) : OrderByDescending(predicate);
    }

    private DbSearchBuilder<T> OrderByDescending(Func<T, object> predicate)
    {
        _enumerable = _enumerable.OrderByDescending(predicate);
        return this;
    }
    
    private DbSearchBuilder<T> OrderByAscending(Func<T, object> predicate)
    {
        _enumerable = _enumerable.OrderBy(predicate);
        
        return this;
    }

    public List<T> BuildAndGetAll()
    {
        
    
        return _enumerable
            .ToList();
    }

    public T? BuildAndGetFirst()
    {
        return _enumerable
            .FirstOrDefault();
    }

    public static DbSearchBuilder<T> NewBuilder(DbSet<T> dbSet)
    {
        return new DbSearchBuilder<T>(dbSet);
    }
    
}