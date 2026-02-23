using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)

{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Sale> Sales { get; set; }

    public DbSet<SaleItem> SaleItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
        modelBuilder
            .Entity<SaleItem>()
            .HasKey(si => new { si.SaleId, si.ProductId });
        
        modelBuilder
            .Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
        
        modelBuilder
            .Entity<Sale>()
            .Property(s => s.TotalPrice)
            .HasPrecision(18, 2);
        
        modelBuilder
            .Entity<SaleItem>()
            .Property(si => si.UnitPriceAtTimeOfSale)
            .HasPrecision(18, 2);
    }
}