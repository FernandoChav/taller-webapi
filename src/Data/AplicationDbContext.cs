using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.src.Models;

namespace Taller1.Data
{
    public class AplicationDbContext(DbContextOptions options) : DbContext
    {
        public DbSet<Product> Products { get; } = null!;
        public DbSet<User> Users { get;  } = null!;
    }
}
