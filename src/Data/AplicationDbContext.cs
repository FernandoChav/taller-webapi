using Microsoft.EntityFrameworkCore;
using Taller1.src.Models;

namespace Taller1.src.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
