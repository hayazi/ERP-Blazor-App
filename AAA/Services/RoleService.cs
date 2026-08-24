using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.AAA.Models;
using ERPBlazorApp.AAA.Data;
using Serilog;

namespace ERPBlazorApp.AAA.Services;

public class RoleService
{
    private readonly AAADbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<RoleService>();

    public RoleService(AAADbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllAsync()
    {
        Logger.Debug("Fetching all roles");
        return await _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching role by id {RoleId}", id);
        return await _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Role role)
    {
        Logger.Information("Adding role {RoleName}", role.Name);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        Logger.Information("Role added with id {RoleId}", role.Id);
    }

    public async Task UpdateAsync(int id, Role role)
    {
        Logger.Information("Updating role {RoleId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Name = role.Name;
        existing.Description = role.Description;
        existing.IsActive = role.IsActive;

        await _context.SaveChangesAsync();
        Logger.Information("Role {RoleId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting role {RoleId}", id);
        var role = await GetByIdAsync(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            Logger.Information("Role {RoleId} deleted", id);
        }
    }

    public async Task<List<Permission>> GetRolePermissionsAsync(int roleId)
    {
        Logger.Debug("Fetching permissions for role {RoleId}", roleId);
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission!)
            .ToListAsync();
    }

    public async Task AssignPermissionAsync(int roleId, int permissionId)
    {
        Logger.Information("Assigning permission {PermissionId} to role {RoleId}", permissionId, roleId);
        var existing = await _context.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        if (existing != null) return;

        _context.RolePermissions.Add(new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId,
            AssignedAt = DateTime.Now
        });
        await _context.SaveChangesAsync();
    }

    public async Task RemovePermissionAsync(int roleId, int permissionId)
    {
        Logger.Information("Removing permission {PermissionId} from role {RoleId}", permissionId, roleId);
        var rolePermission = await _context.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        if (rolePermission != null)
        {
            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
        }
    }
}
