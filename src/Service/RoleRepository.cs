using Taller1.Model;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;


namespace Taller1.Service;

/// <summary>
/// Repository class for managing Role entities.
/// Provides methods for CRUD operations on Role.
/// Implements caching to avoid repeated database lookups.
/// </summary>
public class RoleRepository : IObjectRepository<Role>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Role> _roles;
    private readonly Dictionary<int, Role?> _cache;
    private readonly IUpdateModel<Role> _roleUpdate;

    /// <summary>
    /// Initializes a new instance of the RoleRepository.
    /// </summary>
    /// <param name="applicationDbContext">The database context for interacting with the database.</param>
    /// <param name="roleUpdate">The update model for editing Role entities.</param>
    public RoleRepository(ApplicationDbContext applicationDbContext,
        IUpdateModel<Role> roleUpdate)
    {
        _applicationDbContext = applicationDbContext;
        _roles = applicationDbContext.Roles;
        _cache = new Dictionary<int, Role?>();
        _roleUpdate = roleUpdate;
    }

    /// <summary>
    /// Adds a new role to the database synchronously.
    /// </summary>
    /// <param name="entity">The role entity to be added.</param>
    public void Push(Role entity)
    {
        _roles.Add(entity);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Adds a new role to the database asynchronously.
    /// </summary>
    /// <param name="entity">The role entity to be added.</param>
    /// <returns>The added role entity.</returns>
    public async Task<Role> PushAsync(Role entity)
    {
        await _roles.AddAsync(entity);
        await _applicationDbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a role by its ID synchronously.
    /// </summary>
    /// <param name="roleId">The ID of the role to be deleted.</param>
    /// <returns>The deleted role entity, or null if not found.</returns>
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

    /// <summary>
    /// Deletes a role by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the role to be deleted.</param>
    /// <returns>The deleted role entity, or null if not found.</returns>
    public async Task<Role?> DeleteAsync(int id)
    {
        var role = await AsyncDbSearchBuilder<Role>.NewBuilder(_roles)
            .Filter(role => role.Id == id)
            .BuildAndGetFirst();
        
        
        if (role == null)
        {
            return null;
        }
        
        _roles.Remove(role);
        await _applicationDbContext.SaveChangesAsync();
        return role;
    }

    /// <summary>
    /// Finds a role by its ID synchronously.
    /// Utilizes cache to reduce repeated database lookups.
    /// </summary>
    /// <param name="roleId">The ID of the role to be found.</param>
    /// <returns>The role entity, or null if not found.</returns>
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

    /// <summary>
    /// Finds a role by its ID asynchronously.
    /// This method is not yet implemented.
    /// </summary>
    /// <param name="id">The ID of the role to be found.</param>
    /// <returns>A task that represents the asynchronous operation. Not implemented yet.</returns>
    public Task<Role?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
        /// Edits an existing role by its ID using the specified parameters.
        /// </summary>
        /// <param name="id">The ID of the role to be edited.</param>
        /// <param name="parameters">The parameters for editing the role.</param>
        /// <returns>The edited role entity, or null if the role was not found.</returns>
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