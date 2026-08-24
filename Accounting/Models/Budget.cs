namespace ERPBlazorApp.Accounting.Models;

public class Budget
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public ChartOfAccount? Account { get; set; }
    public int? FiscalYearId { get; set; }
    public FiscalYear? FiscalYear { get; set; }
    public int? AccountingPeriodId { get; set; }
    public AccountingPeriod? AccountingPeriod { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = "Expense"; // Revenue, Expense
}
