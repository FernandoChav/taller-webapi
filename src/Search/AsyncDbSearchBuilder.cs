using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

public class AsyncDbSearchBuilder<T>(DbSet<T> set) where T : class
{
    private IAsyncEnumerable<T> _asyncEnumerable = set.ToAsyncEnumerable();
    public AsyncDbSearchBuilder<T> Filter(Func<T, bool> predicate)
    {
        
        _asyncEnumerable = _asyncEnumerable.Where(predicate);
        return this;
    }
    
    

    public AsyncDbSearchBuilder<T> Page(int page,
        int elements)
    {
        var countElements = (page - 1) * elements;
        _asyncEnumerable = _asyncEnumerable.Skip(countElements);
        return Elements(elements);
    }

    public AsyncDbSearchBuilder<T> Elements(int elements)
    {
        _asyncEnumerable = _asyncEnumerable.Take(elements);
        return this;
    }
    
    public AsyncDbSearchBuilder<T> OrderBy(Func<T, object> predicate, bool ascending)
    {
        return ascending ? OrderByAscending(predicate) : OrderByDescending(predicate);
    }
    
    private AsyncDbSearchBuilder<T> OrderByDescending(Func<T, object> predicate)
    {
        _asyncEnumerable = _asyncEnumerable.OrderByDescending(predicate);
        return this;
    }
    
    private AsyncDbSearchBuilder<T> OrderByAscending(Func<T, object> predicate)
    {
        _asyncEnumerable = _asyncEnumerable.OrderBy(predicate);
        return this;
    }
    
    public async Task<List<T>> BuildAndGetAll()
    {
        return await _asyncEnumerable
            .ToListAsync();
    }

    public async Task<T?> BuildAndGetFirst()
    {
        return await _asyncEnumerable.FirstAsync();
    }

    public static AsyncDbSearchBuilder<T> NewBuilder(DbSet<T> set)
    {
        return new AsyncDbSearchBuilder<T>(set);
    }

    
    
}