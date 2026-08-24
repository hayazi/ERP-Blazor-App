using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.AAA.Models;
using ERPBlazorApp.AAA.Data;
using Serilog;

namespace ERPBlazorApp.AAA.Services;

public class PermissionService
{
    private readonly AAADbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<PermissionService>();

    public PermissionService(AAADbContext context)
    {
        _context = context;
    }

    public async Task<List<Permission>> GetAllAsync()
    {
        Logger.Debug("Fetching all permissions");
        return await _context.Permissions.ToListAsync();
    }

    public async Task<Permission?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching permission by id {PermissionId}", id);
        return await _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Permission permission)
    {
        Logger.Information("Adding permission {PermissionName}", permission.Name);
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
        Logger.Information("Permission added with id {PermissionId}", permission.Id);
    }

    public async Task UpdateAsync(int id, Permission permission)
    {
        Logger.Information("Updating permission {PermissionId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Name = permission.Name;
        existing.Code = permission.Code;
        existing.Description = permission.Description;
        existing.Module = permission.Module;

        await _context.SaveChangesAsync();
        Logger.Information("Permission {PermissionId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting permission {PermissionId}", id);
        var permission = await GetByIdAsync(id);
        if (permission != null)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            Logger.Information("Permission {PermissionId} deleted", id);
        }
    }
}
