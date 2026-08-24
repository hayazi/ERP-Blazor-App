using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public static class AAASampleData
{
    public static List<User> GetUsers()
    {
        return new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@erp.ir", PasswordHash = "admin123", FirstName = "Admin", LastName = "User", IsActive = true },
            new User { Id = 2, Username = "manager", Email = "manager@erp.ir", PasswordHash = "manager123", FirstName = "Manager", LastName = "User", IsActive = true },
            new User { Id = 3, Username = "employee", Email = "employee@erp.ir", PasswordHash = "employee123", FirstName = "Employee", LastName = "User", IsActive = true }
        };
    }

    public static List<Role> GetRoles()
    {
        return new List<Role>
        {
            new Role { Id = 1, Name = "Administrator", Description = "Full system access", IsActive = true },
            new Role { Id = 2, Name = "Manager", Description = "Management access", IsActive = true },
            new Role { Id = 3, Name = "Employee", Description = "Limited access", IsActive = true }
        };
    }

    public static List<Permission> GetPermissions()
    {
        return new List<Permission>
        {
            new Permission { Id = 1, Name = "View Dashboard", Code = "dashboard.view", Description = "View dashboard", Module = "Dashboard" },
            new Permission { Id = 2, Name = "Manage Users", Code = "users.manage", Description = "Create, edit, delete users", Module = "AAA" },
            new Permission { Id = 3, Name = "Manage Roles", Code = "roles.manage", Description = "Create, edit, delete roles", Module = "AAA" },
            new Permission { Id = 4, Name = "View Reports", Code = "reports.view", Description = "View reports", Module = "Reports" },
            new Permission { Id = 5, Name = "Manage Inventory", Code = "inventory.manage", Description = "Manage inventory", Module = "Inventory" }
        };
    }

    public static List<Account> GetAccounts()
    {
        return new List<Account>
        {
            new Account { Id = 1, Code = "1000", Name = "Cash", Type = "Asset", IsActive = true },
            new Account { Id = 2, Code = "1100", Name = "Accounts Receivable", Type = "Asset", IsActive = true },
            new Account { Id = 3, Code = "1200", Name = "Inventory", Type = "Asset", IsActive = true },
            new Account { Id = 4, Code = "2000", Name = "Accounts Payable", Type = "Liability", IsActive = true },
            new Account { Id = 5, Code = "3000", Name = "Equity", Type = "Equity", IsActive = true },
            new Account { Id = 6, Code = "4000", Name = "Sales Revenue", Type = "Revenue", IsActive = true },
            new Account { Id = 7, Code = "5000", Name = "Cost of Goods Sold", Type = "Expense", IsActive = true }
        };
    }

    public static List<Transaction> GetTransactions()
    {
        return new List<Transaction>
        {
            new Transaction { Id = 1, Reference = "TRX-001", Date = DateTime.Today.AddDays(-5), Type = "Journal", Description = "Initial inventory purchase", TotalAmount = 50000000, Status = "Posted" },
            new Transaction { Id = 2, Reference = "TRX-002", Date = DateTime.Today.AddDays(-2), Type = "Receipt", Description = "Customer payment", TotalAmount = 12000000, Status = "Posted" }
        };
    }

    public static List<JournalEntry> GetJournalEntries()
    {
        return new List<JournalEntry>
        {
            new JournalEntry { Id = 1, TransactionId = 1, AccountId = 3, Debit = 50000000, Credit = 0, Description = "Inventory purchase" },
            new JournalEntry { Id = 2, TransactionId = 1, AccountId = 4, Debit = 0, Credit = 50000000, Description = "Accounts payable" },
            new JournalEntry { Id = 3, TransactionId = 2, AccountId = 1, Debit = 12000000, Credit = 0, Description = "Cash received" },
            new JournalEntry { Id = 4, TransactionId = 2, AccountId = 2, Debit = 0, Credit = 12000000, Description = "Accounts receivable cleared" }
        };
    }
}
