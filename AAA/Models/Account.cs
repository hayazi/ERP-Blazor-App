namespace ERPBlazorApp.AAA.Models;

public class Account
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Asset"; // Asset, Liability, Equity, Revenue, Expense
    public int? ParentAccountId { get; set; }
    public Account? ParentAccount { get; set; }
    public List<Account> ChildAccounts { get; set; } = new();
    public bool IsActive { get; set; } = true;
}
