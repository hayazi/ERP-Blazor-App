namespace ERPBlazorApp.Sales.Models;

public class Payment
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = "Cash"; // Cash, Card, Transfer, Check
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded
    public string? ReferenceNumber { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Sale? Sale { get; set; }
}
