namespace ERPBlazorApp.Inventory.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int CurrentStock { get; set; } = 0;
    public int MinStock { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}
