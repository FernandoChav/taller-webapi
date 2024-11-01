using Taller1.Model;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;


namespace Taller1.Service;

public class RoleRepository : IObjectRepository<Role>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Role> _roles;
    private readonly Dictionary<int, Role?> _cache;
    private readonly IUpdateModel<Role> _roleUpdate;

    public RoleRepository(ApplicationDbContext applicationDbContext,
        IUpdateModel<Role> roleUpdate)
    {
        _applicationDbContext = applicationDbContext;
        _roles = applicationDbContext.Roles;
        _cache = new Dictionary<int, Role?>();
        _roleUpdate = roleUpdate;
    }

    public void Push(Role entity)
    {
        _roles.Add(entity);
        _applicationDbContext.SaveChanges();
    }

    public async Task<Role> PushAsync(Role entity)
    {
        await _roles.AddAsync(entity);
        await _applicationDbContext.SaveChangesAsync();
        return entity;
    }

    public Role? Delete(int roleId)
    {
        var role = DbSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        if (role == null)
        {
            return null;
        }
        
        _roles.Remove(role);
        _applicationDbContext.SaveChanges();

        return role;
    }

    public Role? FindById(int roleId)
    {
        if (_cache.TryGetValue(roleId, out Role? roleCached))
        {
            return roleCached;
        }

        var role = DbSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        _cache[roleId] = role;
        return role;
    }

    public Task<Role?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Role? Edit(int id,
        ObjectParameters parameters)
    {
        var role = FindById(id);
        if (role == null)
        {
            return null;
        }

        _roleUpdate.Edit(parameters, role);
        _applicationDbContext.SaveChanges();
        return role;
    }
    
}