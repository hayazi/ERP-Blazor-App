namespace ERPBlazorApp.Inventory.Models;

public class Inbound
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
    public List<InboundDetail> Details { get; set; } = new();
}
