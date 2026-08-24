using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.AAA.Models;
using ERPBlazorApp.AAA.Data;
using Serilog;

namespace ERPBlazorApp.AAA.Services;

public class UserService
{
    private readonly AAADbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<UserService>();

    public UserService(AAADbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        Logger.Debug("Fetching all users");
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching user by id {UserId}", id);
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        Logger.Debug("Fetching user by username {Username}", username);
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        Logger.Information("Adding user {Username}", user.Username);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        Logger.Information("User added with id {UserId}", user.Id);
    }

    public async Task UpdateAsync(int id, User user)
    {
        Logger.Information("Updating user {UserId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Username = user.Username;
        existing.Email = user.Email;
        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.IsActive = user.IsActive;
        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            existing.PasswordHash = user.PasswordHash;
        }

        await _context.SaveChangesAsync();
        Logger.Information("User {UserId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting user {UserId}", id);
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            Logger.Information("User {UserId} deleted", id);
        }
    }

    public async Task<List<Role>> GetUserRolesAsync(int userId)
    {
        Logger.Debug("Fetching roles for user {UserId}", userId);
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role!)
            .ToListAsync();
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        Logger.Information("Assigning role {RoleId} to user {UserId}", roleId, userId);
        var existing = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (existing != null) return;

        _context.UserRoles.Add(new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            AssignedAt = DateTime.Now
        });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoleAsync(int userId, int roleId)
    {
        Logger.Information("Removing role {RoleId} from user {UserId}", roleId, userId);
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
