using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

public class DbSetSearchBuilder<T> where T : class
{
    private Func<T, bool> _predicate;
    private int _page;
    private int _elements = 10;

    private readonly DbSet<T> _dbSet;

    public DbSetSearchBuilder(DbSet<T> dbSet)
    {
        _dbSet = dbSet;
    }

    public DbSetSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        _predicate = predicate;
        return this;
    }

    public DbSetSearchBuilder<T> Page(int page,
        int elements)
    {
        _page = page;
        _elements = elements;
        return this;
    }

    public DbSetSearchBuilder<T> Elements(int elements)
    {
        _elements = elements;
        return this;
    }

    public DbSetSearchBuilder<T> OrderBy()
    {
        return this;
    }

    public List<T> BuildAndGetAll()
    {
        return Build()
            .ToList();
    }

    public T BuildAndGetFirst()
    {
        return Build()
            .First();
    }

    private IEnumerable<T> Build()
    {
        var countElements = (_page - 1) * _elements;
        return _dbSet.Where(_predicate)
            .Skip(countElements)
            .Take(_elements);
    }
}