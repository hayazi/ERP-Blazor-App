namespace ERPBlazorApp.Sales.Models;

public class Sale
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled, Refunded
    public string? Notes { get; set; }
    public int? CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public List<SaleItem> Items { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
}
