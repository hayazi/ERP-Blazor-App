namespace ERPBlazorApp.CRM.Models;

public class Lead
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Status { get; set; } = "New"; // New, Contacted, Qualified, Converted, Lost
    public string Source { get; set; } = string.Empty; // Website, Referral, Social Media, etc.
    public string? Notes { get; set; }
    public int? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
