using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.HumanResource.Models;

namespace ERPBlazorApp.HumanResource.Data;

public class HumanResourceDbContext : DbContext
{
    public HumanResourceDbContext(DbContextOptions<HumanResourceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Attendance> Attendance => Set<Attendance>();
    public DbSet<Leave> Leave => Set<Leave>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Description).HasMaxLength(500);
            entity.Property(d => d.ManagerId).IsRequired(false);
            entity.Property(d => d.ParentDepartmentId).IsRequired(false);

            entity.HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.ParentDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Status).HasMaxLength(20).HasDefaultValue("Present");
            entity.Property(a => a.Notes).HasMaxLength(500);

            entity.HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.LeaveType).HasMaxLength(50).HasDefaultValue("Annual");
            entity.Property(l => l.Status).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(l => l.RequestDate).HasDefaultValueSql("GETDATE()");

            entity.HasOne(l => l.Employee)
                .WithMany()
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
