namespace ERPBlazorApp.HumanResource.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public Employee? Manager { get; set; }
    public int? ParentDepartmentId { get; set; }
    public Department? ParentDepartment { get; set; }
    public List<Department> SubDepartments { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
}
