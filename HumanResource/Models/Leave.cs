namespace ERPBlazorApp.HumanResource.Models;

public class Leave
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public string LeaveType { get; set; } = "Annual";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime RequestDate { get; set; } = DateTime.Now;
}
