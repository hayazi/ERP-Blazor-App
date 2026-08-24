namespace ERPBlazorApp.Accounting.Models;

public class AccountingPeriod
{
    public int Id { get; set; }
    public int FiscalYearId { get; set; }
    public FiscalYear? FiscalYear { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsClosed { get; set; }
}
