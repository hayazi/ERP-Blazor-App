using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public class RoleService
{
    private List<Role> _roles;
    private List<RolePermission> _rolePermissions;

    public RoleService()
    {
        _roles = AAASampleData.GetRoles();
        _rolePermissions = new List<RolePermission>();
    }

    public List<Role> GetAll() => _roles;
    public Role? GetById(int id) => _roles.FirstOrDefault(r => r.Id == id);

    public void Add(Role role)
    {
        role.Id = _roles.Any() ? _roles.Max(r => r.Id) + 1 : 1;
        role.CreatedAt = DateTime.Now;
        _roles.Add(role);
    }

    public void Update(int id, Role role)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = role.Name;
        existing.Description = role.Description;
        existing.IsActive = role.IsActive;
    }

    public void Delete(int id)
    {
        var role = GetById(id);
        if (role != null)
        {
            _roles.Remove(role);
            _rolePermissions.RemoveAll(rp => rp.RoleId == id);
        }
    }

    public List<Permission> GetRolePermissions(int roleId)
    {
        return _rolePermissions.Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission!)
            .ToList();
    }

    public void AssignPermission(int roleId, int permissionId)
    {
        if (_rolePermissions.Any(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)) return;
        _rolePermissions.Add(new RolePermission
        {
            Id = _rolePermissions.Any() ? _rolePermissions.Max(rp => rp.Id) + 1 : 1,
            RoleId = roleId,
            PermissionId = permissionId,
            AssignedAt = DateTime.Now
        });
    }

    public void RemovePermission(int roleId, int permissionId)
    {
        _rolePermissions.RemoveAll(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
    }
}
