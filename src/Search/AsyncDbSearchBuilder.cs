using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Taller1.Search;

public class AsyncDbSearchBuilder<T> where T : class
{
    private IQueryable<T> _set;

    public AsyncDbSearchBuilder(DbSet<T> set)
    {
        _set = set.AsQueryable();
    }

    public AsyncDbSearchBuilder<T> Where(Func<T, bool> predicate)
    {
        _set = _set.Where(predicate)
            .AsQueryable();
        return this;
    }
    
    
    
    public Task<List<T>> Build()
    {
        return _set.
            ToListAsync();
    }
    
}