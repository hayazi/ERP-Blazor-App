namespace ERPBlazorApp.Inventory.Models;

public class OutboundDetail
{
    public int Id { get; set; }
    public int OutboundId { get; set; }
    public Outbound? Outbound { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
