using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.src.Models;

namespace Taller1.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options)
         :   base(options) {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}