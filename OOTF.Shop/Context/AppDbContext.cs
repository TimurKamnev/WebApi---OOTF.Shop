using Microsoft.EntityFrameworkCore;
using OOTF.Shopping.Models;
using System;

namespace OOTF.Shopping.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("ConnectionString");

            optionsBuilder
                .UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Shop>()
                .HasOne(s => s.Manager)
                .WithMany()
                .HasForeignKey(s => s.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Shops)
                .WithMany()
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

        }
    }
}
