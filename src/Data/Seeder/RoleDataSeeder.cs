using Microsoft.EntityFrameworkCore;
using Taller1.Model;

namespace Taller1.Data.Seeder;

public class RoleDataSeeder : IDataSeeder<Role>
{
    
    private readonly DbSet<Role> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public RoleDataSeeder(ApplicationDbContext applicationDbContext)
    {
        _dbSet = applicationDbContext.Roles;
        _applicationDbContext = applicationDbContext;
    }

    public void Seed()
    {
        if (_dbSet.Any())
        {
            return;
        }

        _dbSet.AddRange(new Role
            {
                Id = 0,
                Name = "User"
            },
            new Role
            {
                Id = 1,
                Name = "Administrator"
            });

        _applicationDbContext.SaveChanges();
    }

    public DbSet<Role> Get()
    {
        return _dbSet;
    }
}