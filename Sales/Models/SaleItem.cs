namespace ERPBlazorApp.Sales.Models;

public class SaleItem
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductSku { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string? Notes { get; set; }
    public Sale? Sale { get; set; }
}
