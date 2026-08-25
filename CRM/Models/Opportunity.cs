namespace ERPBlazorApp.CRM.Models;

public class Opportunity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal EstimatedValue { get; set; }
    public string Stage { get; set; } = "Prospecting"; // Prospecting, Qualification, Proposal, Negotiation, Won, Lost
    public string Probability { get; set; } = "10%"; // 10%, 25%, 50%, 75%, 90%, 100%
    public DateTime? ExpectedCloseDate { get; set; }
    public int? LeadId { get; set; }
    public Lead? Lead { get; set; }
    public int? CustomerId { get; set; }
    public int? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
