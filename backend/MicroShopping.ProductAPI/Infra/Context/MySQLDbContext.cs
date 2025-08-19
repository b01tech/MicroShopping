using MicroShopping.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroShopping.ProductAPI.Infra.Context;

public class MySQLDbContext : DbContext
{
    public MySQLDbContext(DbContextOptions options) : base(options) { }

    protected MySQLDbContext() { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.Description)
                .HasMaxLength(300);
            entity.Property(p => p.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            entity.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}
