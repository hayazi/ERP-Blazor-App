using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Sales.Models;

namespace ERPBlazorApp.Sales.Data;

public class SaleDbContext : DbContext
{
    public SaleDbContext(DbContextOptions<SaleDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.InvoiceNumber).IsRequired().HasMaxLength(50);
            entity.Property(s => s.CustomerName).HasMaxLength(100);
            entity.Property(s => s.CustomerPhone).HasMaxLength(20);
            entity.Property(s => s.CustomerEmail).HasMaxLength(100);
            entity.Property(s => s.CustomerAddress).HasMaxLength(200);
            entity.Property(s => s.SubTotal).HasColumnType("decimal(18,2)");
            entity.Property(s => s.TaxAmount).HasColumnType("decimal(18,2)");
            entity.Property(s => s.DiscountAmount).HasColumnType("decimal(18,2)");
            entity.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(s => s.Status).HasMaxLength(20);
            entity.Property(s => s.Notes).HasMaxLength(500);
            entity.Property(s => s.IsActive).HasDefaultValue(true);
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(s => s.SaleDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(si => si.Id);
            entity.Property(si => si.ProductName).HasMaxLength(100);
            entity.Property(si => si.ProductSku).HasMaxLength(50);
            entity.Property(si => si.Quantity).IsRequired();
            entity.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(si => si.TotalPrice).HasColumnType("decimal(18,2)");
            entity.Property(si => si.DiscountAmount).HasColumnType("decimal(18,2)");
            entity.Property(si => si.Notes).HasMaxLength(500);

            entity.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            entity.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(20);
            entity.Property(p => p.Status).HasMaxLength(20);
            entity.Property(p => p.ReferenceNumber).HasMaxLength(100);
            entity.Property(p => p.Notes).HasMaxLength(500);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasOne(p => p.Sale)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
