namespace ERPBlazorApp.CRM.Models;

public class Campaign
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Email"; // Email, Social Media, Event, Advertisement
    public string Status { get; set; } = "Planned"; // Planned, Active, Completed, Cancelled
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }
    public int LeadsGenerated { get; set; }
    public int Conversions { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
