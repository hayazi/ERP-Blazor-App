namespace ERPBlazorApp.Inventory.Models;

public class InboundDetail
{
    public int Id { get; set; }
    public int InboundId { get; set; }
    public Inbound? Inbound { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
