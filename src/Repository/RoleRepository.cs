using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;

namespace Taller1.Repository;

public class RoleRepository
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

    public Role Get(int roleId)
    {
        var roleCached = _cache[roleId];
        if (roleCached != null)
        {
            return roleCached;
        }

        var role = DbSetSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == roleId)
            .BuildAndGetFirst();

        _cache[roleId] = role;
        return role;
    }

    public void Create(Role role)
    {
        _roles.Add(role);
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
    
    
}