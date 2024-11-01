using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.src.Models;

namespace Taller1.Data
{
    
    /// <summary>
    /// This class manage all database, contains every set for handle operations
    /// </summary>
    
    public class ApplicationDbContext : DbContext
    {
        
        /// <summary>
        /// Main constructor 
        /// </summary>
        /// <param name="options">A set options for configuring the database</param>
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Database set for product
        /// </summary>
        
        public DbSet<Product> Products { get; set; } = null;
        
        /// <summary>
        /// Database set for user
        /// </summary>
        
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Database set for roles
        /// </summary>
        
        public DbSet<Role> Roles { get; set; } = null!;

        /// <summary>
        /// Database set for voucher
        /// </summary>
        
        public DbSet<Voucher> Vouchers { get; set; } = null!;

        /// <summary>
        /// Database set for item voucher
        /// </summary>
        
        public DbSet<ItemVoucher> VoucherProducts { get; set; } = null!;

        /// <summary>
        /// This method configure foreing key among relations 
        /// </summary>
        /// <param name="modelBuilder">A model for configuring the relations</param>
        
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voucher>()
                .HasMany(v => v.AllProducts)
                .WithOne(vp => vp.Voucher)
                .HasForeignKey(vp => vp.VoucherId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Vouchers)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<ItemVoucher>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();

        }
        
    }
}