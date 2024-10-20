using Taller1.Model;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;


namespace Taller1.Service;

public class RoleRepository : IObjectRepository<Role, RoleEdit>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Role> _roles;
    private readonly Dictionary<int, Role?> _cache;

    public RoleRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _roles = applicationDbContext.Roles;
        _cache = new Dictionary<int, Role?>();
    }

    public void Push(Role entity)
    {
        _roles.Add(entity);
        _applicationDbContext.SaveChanges();
    }

    public void Delete(int roleId)
    {
        var role = DbSetSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        _roles.Remove(role);
        _applicationDbContext.SaveChanges();
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
            return;
        }

        role.Name = entityEdit.Name;
        _applicationDbContext.SaveChanges();
    }
    
}