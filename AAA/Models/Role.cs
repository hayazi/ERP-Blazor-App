namespace ERPBlazorApp.AAA.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<UserRole> UserRoles { get; set; } = new();
    public List<RolePermission> RolePermissions { get; set; } = new();
}
