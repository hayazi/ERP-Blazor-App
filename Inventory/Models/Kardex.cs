namespace ERPBlazorApp.Inventory.Models;

public class Kardex
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = "Inbound"; // Inbound or Outbound
    public int ReferenceId { get; set; }
    public int Quantity { get; set; }
    public int Balance { get; set; }
    public string? Notes { get; set; }
}
