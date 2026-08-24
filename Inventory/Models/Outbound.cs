namespace ERPBlazorApp.Inventory.Models;

public class Outbound
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
    public List<OutboundDetail> Details { get; set; } = new();
}
