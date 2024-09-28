using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

public class DbSetSearchBuilder<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private IEnumerable<T> _enumerable;

    public DbSetSearchBuilder(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
        _enumerable = _dbSet;
    }

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

    public DbSetSearchBuilder<T> OrderBy()
    {
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
            .First();
    }

    public static DbSetSearchBuilder<T> NewBuilder(DbSet<T> dbSet)
    {
        return new DbSetSearchBuilder<T>(dbSet);
    }
    
}