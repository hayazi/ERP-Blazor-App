namespace ERPBlazorApp.Accounting.Models;

public class TrialBalance
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public ChartOfAccount? Account { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
}
