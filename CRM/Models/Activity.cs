namespace ERPBlazorApp.CRM.Models;

public class Activity
{
    public int Id { get; set; }
    public string Type { get; set; } = "Call"; // Call, Email, Meeting, Task
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, InProgress, Completed, Cancelled
    public int? LeadId { get; set; }
    public Lead? Lead { get; set; }
    public int? CustomerId { get; set; }
    public int? OpportunityId { get; set; }
    public Opportunity? Opportunity { get; set; }
    public int? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
