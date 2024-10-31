using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.src.Models;

namespace Taller1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;

        public DbSet<Voucher> Vouchers { get; set; } = null!;

        public DbSet<ItemVoucher> VoucherProducts { get; set; } = null!;

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