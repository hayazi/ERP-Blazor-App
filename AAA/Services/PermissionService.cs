using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public class PermissionService
{
    private List<Permission> _permissions;

    public PermissionService()
    {
        _permissions = AAASampleData.GetPermissions();
    }

    public List<Permission> GetAll() => _permissions;
    public Permission? GetById(int id) => _permissions.FirstOrDefault(p => p.Id == id);

    public void Add(Permission permission)
    {
        permission.Id = _permissions.Any() ? _permissions.Max(p => p.Id) + 1 : 1;
        _permissions.Add(permission);
    }

    public void Update(int id, Permission permission)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = permission.Name;
        existing.Code = permission.Code;
        existing.Description = permission.Description;
        existing.Module = permission.Module;
    }

    public void Delete(int id)
    {
        var permission = GetById(id);
        if (permission != null)
        {
            _permissions.Remove(permission);
        }
    }
}
