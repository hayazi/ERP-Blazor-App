using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Inventory.Models;

namespace ERPBlazorApp.Inventory.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Inbound> Inbounds => Set<Inbound>();
    public DbSet<InboundDetail> InboundDetails => Set<InboundDetail>();
    public DbSet<Outbound> Outbounds => Set<Outbound>();
    public DbSet<OutboundDetail> OutboundDetails => Set<OutboundDetail>();
    public DbSet<Kardex> Kardex => Set<Kardex>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.SKU).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Unit).IsRequired().HasMaxLength(20);
            entity.Property(p => p.PurchasePrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.SalePrice).HasColumnType("decimal(18,2)");
            entity.Property(p => p.IsActive).HasDefaultValue(true);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.Property(s => s.ContactName).HasMaxLength(100);
            entity.Property(s => s.Phone).HasMaxLength(20);
            entity.Property(s => s.Email).HasMaxLength(100);
            entity.Property(s => s.Address).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.ContactName).HasMaxLength(100);
            entity.Property(c => c.Phone).HasMaxLength(20);
            entity.Property(c => c.Email).HasMaxLength(100);
            entity.Property(c => c.Address).HasMaxLength(200);
        });

        modelBuilder.Entity<Inbound>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Reference).IsRequired().HasMaxLength(50);
            entity.Property(i => i.Status).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(i => i.Notes).HasMaxLength(500);

            entity.HasOne(i => i.Supplier)
                .WithMany()
                .HasForeignKey(i => i.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InboundDetail>(entity =>
        {
            entity.HasKey(id => id.Id);
            entity.Property(id => id.Quantity).IsRequired();
            entity.Property(id => id.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(id => id.TotalPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(id => id.Inbound)
                .WithMany(i => i.Details)
                .HasForeignKey(id => id.InboundId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(id => id.Product)
                .WithMany()
                .HasForeignKey(id => id.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Outbound>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Reference).IsRequired().HasMaxLength(50);
            entity.Property(o => o.Status).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(o => o.Notes).HasMaxLength(500);

            entity.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OutboundDetail>(entity =>
        {
            entity.HasKey(od => od.Id);
            entity.Property(od => od.Quantity).IsRequired();
            entity.Property(od => od.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(od => od.TotalPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(od => od.Outbound)
                .WithMany(o => o.Details)
                .HasForeignKey(od => od.OutboundId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(k => k.Type).HasMaxLength(20).HasDefaultValue("Inbound");
            entity.Property(k => k.Notes).HasMaxLength(500);

            entity.HasOne(k => k.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
