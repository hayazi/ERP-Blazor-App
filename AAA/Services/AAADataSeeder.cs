using ERPBlazorApp.AAA.Data;
using ERPBlazorApp.AAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ERPBlazorApp.AAA.Services;

public static class AAADataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AAADbContext>();

        if (await context.Users.AnyAsync())
        {
            return;
        }

        var roles = new List<Role>
        {
            new Role { Name = "Administrator", Description = "Full system access", IsActive = true },
            new Role { Name = "Manager", Description = "Management access", IsActive = true },
            new Role { Name = "Employee", Description = "Limited access", IsActive = true }
        };

        context.Roles.AddRange(roles);
        await context.SaveChangesAsync();

        var permissions = new List<Permission>
        {
            new Permission { Name = "View Dashboard", Code = "dashboard.view", Description = "View dashboard", Module = "Dashboard" },
            new Permission { Name = "Manage Users", Code = "users.manage", Description = "Create, edit, delete users", Module = "AAA" },
            new Permission { Name = "Manage Roles", Code = "roles.manage", Description = "Create, edit, delete roles", Module = "AAA" },
            new Permission { Name = "View Reports", Code = "reports.view", Description = "View reports", Module = "Reports" },
            new Permission { Name = "Manage Inventory", Code = "inventory.manage", Description = "Manage inventory", Module = "Inventory" }
        };

        context.Permissions.AddRange(permissions);
        await context.SaveChangesAsync();

        var users = new List<User>
        {
            new User { Username = "admin", Email = "admin@erp.ir", PasswordHash = "admin123", FirstName = "Admin", LastName = "User", IsActive = true },
            new User { Username = "manager", Email = "manager@erp.ir", PasswordHash = "manager123", FirstName = "Manager", LastName = "User", IsActive = true },
            new User { Username = "employee", Email = "employee@erp.ir", PasswordHash = "employee123", FirstName = "Employee", LastName = "User", IsActive = true }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        var accounts = new List<Account>
        {
            new Account { Code = "1000", Name = "Cash", Type = "Asset", IsActive = true },
            new Account { Code = "1100", Name = "Accounts Receivable", Type = "Asset", IsActive = true },
            new Account { Code = "1200", Name = "Inventory", Type = "Asset", IsActive = true },
            new Account { Code = "2000", Name = "Accounts Payable", Type = "Liability", IsActive = true },
            new Account { Code = "3000", Name = "Equity", Type = "Equity", IsActive = true },
            new Account { Code = "4000", Name = "Sales Revenue", Type = "Revenue", IsActive = true },
            new Account { Code = "5000", Name = "Cost of Goods Sold", Type = "Expense", IsActive = true }
        };

        context.Accounts.AddRange(accounts);
        await context.SaveChangesAsync();

        var userRoles = new List<UserRole>
        {
            new UserRole { UserId = users[0].Id, RoleId = roles[0].Id },
            new UserRole { UserId = users[1].Id, RoleId = roles[1].Id },
            new UserRole { UserId = users[2].Id, RoleId = roles[2].Id }
        };

        context.UserRoles.AddRange(userRoles);
        await context.SaveChangesAsync();

        var rolePermissions = new List<RolePermission>
        {
            new RolePermission { RoleId = roles[0].Id, PermissionId = permissions[0].Id },
            new RolePermission { RoleId = roles[0].Id, PermissionId = permissions[1].Id },
            new RolePermission { RoleId = roles[0].Id, PermissionId = permissions[2].Id },
            new RolePermission { RoleId = roles[0].Id, PermissionId = permissions[3].Id },
            new RolePermission { RoleId = roles[0].Id, PermissionId = permissions[4].Id },
            new RolePermission { RoleId = roles[1].Id, PermissionId = permissions[0].Id },
            new RolePermission { RoleId = roles[1].Id, PermissionId = permissions[3].Id },
            new RolePermission { RoleId = roles[2].Id, PermissionId = permissions[0].Id }
        };

        context.RolePermissions.AddRange(rolePermissions);
        await context.SaveChangesAsync();
    }
}
