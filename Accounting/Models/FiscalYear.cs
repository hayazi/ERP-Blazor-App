namespace ERPBlazorApp.Accounting.Models;

public class FiscalYear
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsClosed { get; set; }
    public List<AccountingPeriod> Periods { get; set; } = new();
}
