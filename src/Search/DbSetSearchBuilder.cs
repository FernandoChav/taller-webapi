using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

/// <summary>
/// This class is used for create a query
/// this use a build design pattern
/// </summary>
/// <typeparam name="T">A Object retrieve</typeparam>

public class DbSetSearchBuilder<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private IEnumerable<T> _enumerable;

    public DbSetSearchBuilder(DbSet<T> dbSet)
    {
        
        _dbSet = dbSet;
        _enumerable = _dbSet;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    
    public DbSetSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        
        _enumerable = _dbSet.Where(predicate);
        return this;
    }

    public DbSetSearchBuilder<T> Page(int page,
        int elements)
    {
        var countElements = (page - 1) * elements;
        _enumerable = _enumerable.Skip(countElements);
        return Elements(elements);
    }

    public DbSetSearchBuilder<T> Elements(int elements)
    {
        _enumerable = _enumerable.Take(elements);
        return this;
    }

    public DbSetSearchBuilder<T> OrderBy(Func<T, object> predicate, bool ascending)
    {
        return ascending ? OrderByAscending(predicate) : OrderByDescending(predicate);
    }

    public DbSetSearchBuilder<T> OrderByDescending(Func<T, object> predicate)
    {
        _enumerable = _enumerable.OrderByDescending(predicate);
        return this;
    }
    
    public DbSetSearchBuilder<T> OrderByAscending(Func<T, object> predicate)
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

    public static DbSetSearchBuilder<T> NewBuilder(DbSet<T> dbSet)
    {
        return new DbSetSearchBuilder<T>(dbSet);
    }
    
}