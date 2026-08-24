namespace ERPBlazorApp.AAA.Models;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Module { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = new();
}
