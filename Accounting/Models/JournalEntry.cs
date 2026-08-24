namespace ERPBlazorApp.Accounting.Models;

public class JournalEntry
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int? FiscalYearId { get; set; }
    public FiscalYear? FiscalYear { get; set; }
    public int? AccountingPeriodId { get; set; }
    public AccountingPeriod? AccountingPeriod { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public string Status { get; set; } = "Draft"; // Draft, Posted, Reversed
    public List<JournalEntryLine> JournalEntryLines { get; set; } = new();
}
