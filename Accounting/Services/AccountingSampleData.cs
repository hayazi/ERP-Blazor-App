using ERPBlazorApp.Accounting.Models;

namespace ERPBlazorApp.Accounting.Services;

public static class AccountingSampleData
{
    public static List<ChartOfAccount> GetAccounts()
    {
        return new List<ChartOfAccount>
        {
            new ChartOfAccount { Id = 1, Code = "1000", Name = "Cash", Type = "Asset", IsActive = true },
            new ChartOfAccount { Id = 2, Code = "1100", Name = "Accounts Receivable", Type = "Asset", IsActive = true },
            new ChartOfAccount { Id = 3, Code = "1200", Name = "Inventory", Type = "Asset", IsActive = true },
            new ChartOfAccount { Id = 4, Code = "2000", Name = "Accounts Payable", Type = "Liability", IsActive = true },
            new ChartOfAccount { Id = 5, Code = "3000", Name = "Equity", Type = "Equity", IsActive = true },
            new ChartOfAccount { Id = 6, Code = "4000", Name = "Sales Revenue", Type = "Revenue", IsActive = true },
            new ChartOfAccount { Id = 7, Code = "5000", Name = "Cost of Goods Sold", Type = "Expense", IsActive = true }
        };
    }

    public static List<FiscalYear> GetFiscalYears()
    {
        return new List<FiscalYear>
        {
            new FiscalYear { Id = 1, Name = "1404", StartDate = new DateTime(2025, 3, 21), EndDate = new DateTime(2026, 3, 20), IsActive = true, IsClosed = false },
            new FiscalYear { Id = 2, Name = "1403", StartDate = new DateTime(2024, 3, 21), EndDate = new DateTime(2025, 3, 20), IsActive = false, IsClosed = true }
        };
    }

    public static List<AccountingPeriod> GetPeriods()
    {
        return new List<AccountingPeriod>
        {
            new AccountingPeriod { Id = 1, FiscalYearId = 1, Name = "Farvardin", StartDate = new DateTime(2025, 3, 21), EndDate = new DateTime(2025, 4, 20), IsClosed = false },
            new AccountingPeriod { Id = 2, FiscalYearId = 1, Name = "Ordibehesht", StartDate = new DateTime(2025, 4, 21), EndDate = new DateTime(2025, 5, 21), IsClosed = false }
        };
    }

    public static List<JournalEntry> GetJournalEntries()
    {
        return new List<JournalEntry>
        {
            new JournalEntry { Id = 1, Reference = "JRN-001", Date = DateTime.Today.AddDays(-5), Description = "Initial inventory purchase", TotalDebit = 50000000, TotalCredit = 50000000, Status = "Posted", FiscalYearId = 1, AccountingPeriodId = 1 },
            new JournalEntry { Id = 2, Reference = "JRN-002", Date = DateTime.Today.AddDays(-2), Description = "Customer payment received", TotalDebit = 12000000, TotalCredit = 12000000, Status = "Posted", FiscalYearId = 1, AccountingPeriodId = 1 }
        };
    }

    public static List<JournalEntryLine> GetJournalEntryLines()
    {
        return new List<JournalEntryLine>
        {
            new JournalEntryLine { Id = 1, JournalEntryId = 1, AccountId = 3, Debit = 50000000, Credit = 0, Description = "Inventory purchase" },
            new JournalEntryLine { Id = 2, JournalEntryId = 1, AccountId = 4, Debit = 0, Credit = 50000000, Description = "Accounts payable" },
            new JournalEntryLine { Id = 3, JournalEntryId = 2, AccountId = 1, Debit = 12000000, Credit = 0, Description = "Cash received" },
            new JournalEntryLine { Id = 4, JournalEntryId = 2, AccountId = 2, Debit = 0, Credit = 12000000, Description = "Accounts receivable cleared" }
        };
    }

    public static List<Budget> GetBudgets()
    {
        return new List<Budget>
        {
            new Budget { Id = 1, AccountId = 7, FiscalYearId = 1, Amount = 100000000, Type = "Expense" },
            new Budget { Id = 2, AccountId = 6, FiscalYearId = 1, Amount = 200000000, Type = "Revenue" }
        };
    }

    public static List<TrialBalance> GetTrialBalance()
    {
        return new List<TrialBalance>
        {
            new TrialBalance { Id = 1, AccountId = 1, Debit = 12000000, Credit = 0, Balance = 12000000 },
            new TrialBalance { Id = 2, AccountId = 2, Debit = 0, Credit = 12000000, Balance = -12000000 },
            new TrialBalance { Id = 3, AccountId = 3, Debit = 50000000, Credit = 0, Balance = 50000000 },
            new TrialBalance { Id = 4, AccountId = 4, Debit = 0, Credit = 50000000, Balance = -50000000 }
        };
    }
}
