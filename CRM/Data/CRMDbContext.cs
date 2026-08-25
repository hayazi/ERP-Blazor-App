using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.CRM.Models;

namespace ERPBlazorApp.CRM.Data;

public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions<CRMDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Opportunity> Opportunities => Set<Opportunity>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(l => l.LastName).IsRequired().HasMaxLength(100);
            entity.Property(l => l.Email).IsRequired().HasMaxLength(100);
            entity.Property(l => l.Phone).IsRequired().HasMaxLength(50);
            entity.Property(l => l.Company).HasMaxLength(100);
            entity.Property(l => l.Status).HasMaxLength(50);
            entity.Property(l => l.Source).HasMaxLength(50);
            entity.Property(l => l.Notes).HasMaxLength(1000);
            entity.Property(l => l.IsActive).HasDefaultValue(true);
            entity.Property(l => l.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Title).IsRequired().HasMaxLength(200);
            entity.Property(o => o.Description).HasMaxLength(1000);
            entity.Property(o => o.EstimatedValue).HasColumnType("decimal(18,2)");
            entity.Property(o => o.Stage).HasMaxLength(50);
            entity.Property(o => o.Probability).HasMaxLength(10);
            entity.Property(o => o.IsActive).HasDefaultValue(true);
            entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasOne(o => o.Lead)
                .WithMany()
                .HasForeignKey(o => o.LeadId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Type).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Subject).IsRequired().HasMaxLength(200);
            entity.Property(a => a.Description).HasMaxLength(1000);
            entity.Property(a => a.Status).HasMaxLength(50);
            entity.Property(a => a.IsActive).HasDefaultValue(true);
            entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasOne(a => a.Lead)
                .WithMany()
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Opportunity)
                .WithMany()
                .HasForeignKey(a => a.OpportunityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Type).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Status).HasMaxLength(50);
            entity.Property(c => c.Description).HasMaxLength(1000);
            entity.Property(c => c.Budget).HasColumnType("decimal(18,2)");
            entity.Property(c => c.IsActive).HasDefaultValue(true);
            entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
        });
    }
}
