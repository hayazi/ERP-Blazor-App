using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.Accounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ERPBlazorApp.Accounting.Services;

public static class AccountingDataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();

        if (await context.FiscalYears.AnyAsync())
        {
            return;
        }

        var fiscalYears = new List<FiscalYear>
        {
            new FiscalYear { Name = "1404", StartDate = new DateTime(2025, 3, 21), EndDate = new DateTime(2026, 3, 20), IsActive = true, IsClosed = false },
            new FiscalYear { Name = "1403", StartDate = new DateTime(2024, 3, 21), EndDate = new DateTime(2025, 3, 20), IsActive = false, IsClosed = true }
        };

        context.FiscalYears.AddRange(fiscalYears);
        await context.SaveChangesAsync();

        var periods = new List<AccountingPeriod>
        {
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Farvardin", StartDate = new DateTime(2025, 3, 21), EndDate = new DateTime(2025, 4, 20), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Ordibehesht", StartDate = new DateTime(2025, 4, 21), EndDate = new DateTime(2025, 5, 21), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Khordad", StartDate = new DateTime(2025, 5, 22), EndDate = new DateTime(2025, 6, 21), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Tir", StartDate = new DateTime(2025, 6, 22), EndDate = new DateTime(2025, 7, 22), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Mordad", StartDate = new DateTime(2025, 7, 23), EndDate = new DateTime(2025, 8, 22), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Shahrivar", StartDate = new DateTime(2025, 8, 23), EndDate = new DateTime(2025, 9, 22), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Mehr", StartDate = new DateTime(2025, 9, 23), EndDate = new DateTime(2025, 10, 22), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Aban", StartDate = new DateTime(2025, 10, 23), EndDate = new DateTime(2025, 11, 21), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Azar", StartDate = new DateTime(2025, 11, 22), EndDate = new DateTime(2025, 12, 21), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Dey", StartDate = new DateTime(2025, 12, 22), EndDate = new DateTime(2026, 1, 20), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Bahman", StartDate = new DateTime(2026, 1, 21), EndDate = new DateTime(2026, 2, 19), IsClosed = false },
            new AccountingPeriod { FiscalYearId = fiscalYears[0].Id, Name = "Esfand", StartDate = new DateTime(2026, 2, 20), EndDate = new DateTime(2026, 3, 20), IsClosed = false }
        };

        context.AccountingPeriods.AddRange(periods);
        await context.SaveChangesAsync();

        var parentAccounts = new List<ChartOfAccount>
        {
            new ChartOfAccount { Code = "100", Name = "Current Assets", Type = "Asset", IsActive = true },
            new ChartOfAccount { Code = "200", Name = "Current Liabilities", Type = "Liability", IsActive = true },
            new ChartOfAccount { Code = "400", Name = "Operating Revenue", Type = "Revenue", IsActive = true },
            new ChartOfAccount { Code = "500", Name = "Operating Expenses", Type = "Expense", IsActive = true }
        };

        context.ChartOfAccounts.AddRange(parentAccounts);
        await context.SaveChangesAsync();

        var currentAssetsId = parentAccounts.First(a => a.Code == "100").Id;
        var currentLiabilitiesId = parentAccounts.First(a => a.Code == "200").Id;
        var operatingRevenueId = parentAccounts.First(a => a.Code == "400").Id;
        var operatingExpensesId = parentAccounts.First(a => a.Code == "500").Id;

        var chartOfAccounts = new List<ChartOfAccount>
        {
            new ChartOfAccount { Code = "1000", Name = "Cash", Type = "Asset", IsActive = true, ParentAccountId = currentAssetsId },
            new ChartOfAccount { Code = "1100", Name = "Accounts Receivable", Type = "Asset", IsActive = true, ParentAccountId = currentAssetsId },
            new ChartOfAccount { Code = "1200", Name = "Inventory", Type = "Asset", IsActive = true, ParentAccountId = currentAssetsId },
            new ChartOfAccount { Code = "1300", Name = "Prepaid Expenses", Type = "Asset", IsActive = true, ParentAccountId = currentAssetsId },
            new ChartOfAccount { Code = "1500", Name = "Fixed Assets", Type = "Asset", IsActive = true },
            new ChartOfAccount { Code = "2000", Name = "Accounts Payable", Type = "Liability", IsActive = true, ParentAccountId = currentLiabilitiesId },
            new ChartOfAccount { Code = "2100", Name = "Accrued Expenses", Type = "Liability", IsActive = true, ParentAccountId = currentLiabilitiesId },
            new ChartOfAccount { Code = "2500", Name = "Long Term Debt", Type = "Liability", IsActive = true },
            new ChartOfAccount { Code = "3000", Name = "Equity", Type = "Equity", IsActive = true },
            new ChartOfAccount { Code = "4000", Name = "Sales Revenue", Type = "Revenue", IsActive = true, ParentAccountId = operatingRevenueId },
            new ChartOfAccount { Code = "4100", Name = "Service Revenue", Type = "Revenue", IsActive = true, ParentAccountId = operatingRevenueId },
            new ChartOfAccount { Code = "5000", Name = "Cost of Goods Sold", Type = "Expense", IsActive = true, ParentAccountId = operatingExpensesId },
            new ChartOfAccount { Code = "5100", Name = "Salaries Expense", Type = "Expense", IsActive = true, ParentAccountId = operatingExpensesId },
            new ChartOfAccount { Code = "5200", Name = "Rent Expense", Type = "Expense", IsActive = true, ParentAccountId = operatingExpensesId },
            new ChartOfAccount { Code = "5300", Name = "Utilities Expense", Type = "Expense", IsActive = true, ParentAccountId = operatingExpensesId }
        };

        context.ChartOfAccounts.AddRange(chartOfAccounts);
        await context.SaveChangesAsync();

        var currentAssets = chartOfAccounts.First(a => a.Code == "1000");
        var currentLiabilities = chartOfAccounts.First(a => a.Code == "2000");
        var operatingRevenue = chartOfAccounts.First(a => a.Code == "4000");
        var operatingExpenses = chartOfAccounts.First(a => a.Code == "5000");

        chartOfAccounts.First(a => a.Code == "1100").ParentAccountId = currentAssets.Id;
        chartOfAccounts.First(a => a.Code == "1200").ParentAccountId = currentAssets.Id;
        chartOfAccounts.First(a => a.Code == "1300").ParentAccountId = currentAssets.Id;
        chartOfAccounts.First(a => a.Code == "1500").ParentAccountId = currentAssets.Id;
        chartOfAccounts.First(a => a.Code == "2100").ParentAccountId = currentLiabilities.Id;
        chartOfAccounts.First(a => a.Code == "2500").ParentAccountId = currentLiabilities.Id;
        chartOfAccounts.First(a => a.Code == "4100").ParentAccountId = operatingRevenue.Id;
        chartOfAccounts.First(a => a.Code == "5100").ParentAccountId = operatingExpenses.Id;
        chartOfAccounts.First(a => a.Code == "5200").ParentAccountId = operatingExpenses.Id;
        chartOfAccounts.First(a => a.Code == "5300").ParentAccountId = operatingExpenses.Id;

        await context.SaveChangesAsync();

        var journalEntries = new List<JournalEntry>
        {
            new JournalEntry { Reference = "JRN-001", Date = new DateTime(2025, 3, 25), Description = "Initial inventory purchase", TotalDebit = 50000000, TotalCredit = 50000000, Status = "Posted", FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[0].Id },
            new JournalEntry { Reference = "JRN-002", Date = new DateTime(2025, 4, 5), Description = "Customer payment received", TotalDebit = 12000000, TotalCredit = 12000000, Status = "Posted", FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[1].Id },
            new JournalEntry { Reference = "JRN-003", Date = new DateTime(2025, 4, 10), Description = "Office rent payment", TotalDebit = 5000000, TotalCredit = 5000000, Status = "Posted", FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[1].Id },
            new JournalEntry { Reference = "JRN-004", Date = new DateTime(2025, 5, 1), Description = "Sales revenue", TotalDebit = 0, TotalCredit = 25000000, Status = "Draft", FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[2].Id }
        };

        context.JournalEntries.AddRange(journalEntries);
        await context.SaveChangesAsync();

        var journalEntryLines = new List<JournalEntryLine>
        {
            new JournalEntryLine { JournalEntryId = journalEntries[0].Id, AccountId = chartOfAccounts.First(a => a.Code == "1200").Id, Debit = 50000000, Credit = 0, Description = "Inventory purchase" },
            new JournalEntryLine { JournalEntryId = journalEntries[0].Id, AccountId = chartOfAccounts.First(a => a.Code == "2000").Id, Debit = 0, Credit = 50000000, Description = "Accounts payable" },
            new JournalEntryLine { JournalEntryId = journalEntries[1].Id, AccountId = chartOfAccounts.First(a => a.Code == "1000").Id, Debit = 12000000, Credit = 0, Description = "Cash received" },
            new JournalEntryLine { JournalEntryId = journalEntries[1].Id, AccountId = chartOfAccounts.First(a => a.Code == "1100").Id, Debit = 0, Credit = 12000000, Description = "Accounts receivable cleared" },
            new JournalEntryLine { JournalEntryId = journalEntries[2].Id, AccountId = chartOfAccounts.First(a => a.Code == "5200").Id, Debit = 5000000, Credit = 0, Description = "Rent expense" },
            new JournalEntryLine { JournalEntryId = journalEntries[2].Id, AccountId = chartOfAccounts.First(a => a.Code == "1000").Id, Debit = 0, Credit = 5000000, Description = "Cash paid" },
            new JournalEntryLine { JournalEntryId = journalEntries[3].Id, AccountId = chartOfAccounts.First(a => a.Code == "1000").Id, Debit = 25000000, Credit = 0, Description = "Cash received" },
            new JournalEntryLine { JournalEntryId = journalEntries[3].Id, AccountId = chartOfAccounts.First(a => a.Code == "4000").Id, Debit = 0, Credit = 25000000, Description = "Sales revenue" }
        };

        context.JournalEntryLines.AddRange(journalEntryLines);
        await context.SaveChangesAsync();

        var budgets = new List<Budget>
        {
            new Budget { AccountId = chartOfAccounts.First(a => a.Code == "5000").Id, FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[0].Id, Amount = 100000000, Type = "Expense" },
            new Budget { AccountId = chartOfAccounts.First(a => a.Code == "5100").Id, FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[0].Id, Amount = 300000000, Type = "Expense" },
            new Budget { AccountId = chartOfAccounts.First(a => a.Code == "4000").Id, FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[0].Id, Amount = 500000000, Type = "Revenue" },
            new Budget { AccountId = chartOfAccounts.First(a => a.Code == "4100").Id, FiscalYearId = fiscalYears[0].Id, AccountingPeriodId = periods[0].Id, Amount = 200000000, Type = "Revenue" }
        };

        context.Budgets.AddRange(budgets);
        await context.SaveChangesAsync();

        var trialBalances = new List<TrialBalance>
        {
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "1000").Id, Debit = 37000000, Credit = 0, Balance = 37000000 },
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "1100").Id, Debit = 0, Credit = 12000000, Balance = -12000000 },
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "1200").Id, Debit = 50000000, Credit = 0, Balance = 50000000 },
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "2000").Id, Debit = 0, Credit = 50000000, Balance = -50000000 },
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "4000").Id, Debit = 0, Credit = 25000000, Balance = -25000000 },
            new TrialBalance { AccountId = chartOfAccounts.First(a => a.Code == "5200").Id, Debit = 5000000, Credit = 0, Balance = 5000000 }
        };

        context.TrialBalances.AddRange(trialBalances);
        await context.SaveChangesAsync();
    }
}
