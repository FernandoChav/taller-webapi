using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Taller1.Model;

namespace Taller1.Data.Seeder;
/// <summary>
/// Seeder class responsible for populating the Roles table with initial role data.
/// It adds predefined roles to the Roles table if no data is present.
/// </summary>
public class RoleDataSeeder : IDataSeeder<Role>
{
    
    private readonly DbSet<Role> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDataSeeder"/> class.
    /// </summary>
    /// <param name="applicationDbContext">The instance of the application database context to interact with the database.</param>
    public RoleDataSeeder(ApplicationDbContext applicationDbContext)
    {
        _dbSet = applicationDbContext.Roles;
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// Seeds the Roles table with default data if it is empty.
    /// It adds two predefined roles: "User" and "Administrator".
    /// </summary>
    public void Seed()
    {
        if (_dbSet.Any())
        {
            return;
        }

        _dbSet.AddRange(new Role
            {
                Name = "User"
            },
            new Role
            {
                Name = "Administrator"
            });

        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Retrieves the DbSet of Roles from the database.
    /// </summary>
    /// <returns>The DbSet representing the Roles table.</returns>
    public DbSet<Role> Get()
    {
        return _dbSet;
    }
}