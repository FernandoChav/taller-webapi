using Taller1.Model;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.TException;
using Taller1.Update;


namespace Taller1.Service;

public class RoleRepository : IObjectRepository<Role, RoleEdit>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Role> _roles;
    private readonly Dictionary<int, Role?> _cache;
    private readonly IUpdateModel<RoleEdit, Role> _roleUpdate;

    public RoleRepository(ApplicationDbContext applicationDbContext,
        IUpdateModel<RoleEdit, Role> roleUpdate)
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

    public Role Delete(int roleId)
    {
        var role = DbSetSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        if (role == null)
        {
            throw new ElementNotFound();
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

        var role = DbSetSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        _cache[roleId] = role;
        return role;
    }

    public void Edit(int id,
        RoleEdit entityEdit)
    {
        var role = FindById(id);
        if (role == null)
        {
            throw new ElementNotFound();
        }

        _roleUpdate.Edit(entityEdit, role);
        _applicationDbContext.SaveChanges();
    }
    
}