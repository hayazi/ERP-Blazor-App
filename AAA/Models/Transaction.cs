namespace ERPBlazorApp.AAA.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = "Journal"; // Journal, Payment, Receipt
    public string Description { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public List<JournalEntry> JournalEntries { get; set; } = new();
}
