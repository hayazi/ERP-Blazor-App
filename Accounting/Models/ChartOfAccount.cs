namespace ERPBlazorApp.Accounting.Models;

public class ChartOfAccount
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Asset"; // Asset, Liability, Equity, Revenue, Expense
    public int? ParentAccountId { get; set; }
    public ChartOfAccount? ParentAccount { get; set; }
    public List<ChartOfAccount> ChildAccounts { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public decimal? CurrentBalance { get; set; }
}
