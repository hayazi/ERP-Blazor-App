using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;

namespace ERPBlazorApp.Accounting.Data;

public class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();
    public DbSet<FiscalYear> FiscalYears => Set<FiscalYear>();
    public DbSet<AccountingPeriod> AccountingPeriods => Set<AccountingPeriod>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalEntryLine> JournalEntryLines => Set<JournalEntryLine>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<TrialBalance> TrialBalances => Set<TrialBalance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChartOfAccount>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Code).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Type).IsRequired().HasMaxLength(20);
            entity.Property(c => c.IsActive).HasDefaultValue(true);
            entity.Property(c => c.CurrentBalance).HasColumnType("decimal(18,2)");

            entity.HasOne(c => c.ParentAccount)
                .WithMany(c => c.ChildAccounts)
                .HasForeignKey(c => c.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FiscalYear>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Name).IsRequired().HasMaxLength(20);
            entity.Property(f => f.IsActive).HasDefaultValue(false);
            entity.Property(f => f.IsClosed).HasDefaultValue(false);
        });

        modelBuilder.Entity<AccountingPeriod>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(50);
            entity.Property(a => a.IsClosed).HasDefaultValue(false);

            entity.HasOne(a => a.FiscalYear)
                .WithMany(f => f.Periods)
                .HasForeignKey(a => a.FiscalYearId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(j => j.Id);
            entity.Property(j => j.Reference).IsRequired().HasMaxLength(50);
            entity.Property(j => j.Description).HasMaxLength(500);
            entity.Property(j => j.TotalDebit).HasColumnType("decimal(18,2)");
            entity.Property(j => j.TotalCredit).HasColumnType("decimal(18,2)");
            entity.Property(j => j.Status).HasMaxLength(20).HasDefaultValue("Draft");

            entity.HasOne(j => j.FiscalYear)
                .WithMany()
                .HasForeignKey(j => j.FiscalYearId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(j => j.AccountingPeriod)
                .WithMany()
                .HasForeignKey(j => j.AccountingPeriodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<JournalEntryLine>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Description).HasMaxLength(500);
            entity.Property(l => l.Debit).HasColumnType("decimal(18,2)");
            entity.Property(l => l.Credit).HasColumnType("decimal(18,2)");

            entity.HasOne(l => l.JournalEntry)
                .WithMany(j => j.JournalEntryLines)
                .HasForeignKey(l => l.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.Account)
                .WithMany()
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Amount).HasColumnType("decimal(18,2)");
            entity.Property(b => b.Type).HasMaxLength(20).HasDefaultValue("Expense");

            entity.HasOne(b => b.Account)
                .WithMany()
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.FiscalYear)
                .WithMany()
                .HasForeignKey(b => b.FiscalYearId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.AccountingPeriod)
                .WithMany()
                .HasForeignKey(b => b.AccountingPeriodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TrialBalance>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Debit).HasColumnType("decimal(18,2)");
            entity.Property(t => t.Credit).HasColumnType("decimal(18,2)");
            entity.Property(t => t.Balance).HasColumnType("decimal(18,2)");

            entity.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
