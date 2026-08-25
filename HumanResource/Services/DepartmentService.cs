using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.HumanResource.Models;
using ERPBlazorApp.HumanResource.Data;
using Serilog;

namespace ERPBlazorApp.HumanResource.Services;

public class DepartmentService
{
    private readonly HumanResourceDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<DepartmentService>();

    public DepartmentService(HumanResourceDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAllRootsAsync()
    {
        Logger.Debug("Fetching all root departments");
        var all = await _context.Departments
            .Include(d => d.Manager)
            .Include(d => d.Employees)
            .ToListAsync();

        var lookup = all.ToDictionary(d => d.Id);
        foreach (var dept in all)
        {
            if (dept.ParentDepartmentId.HasValue && lookup.ContainsKey(dept.ParentDepartmentId.Value))
            {
                var parent = lookup[dept.ParentDepartmentId.Value];
                if (!parent.SubDepartments.Any(sd => sd.Id == dept.Id))
                {
                    parent.SubDepartments.Add(dept);
                }
            }
        }

        var roots = all.Where(d => !d.ParentDepartmentId.HasValue).ToList();
        Logger.Information("Retrieved {Count} root departments", roots.Count);
        return roots;
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching department by id {DepartmentId}", id);
        return await _context.Departments
            .Include(d => d.Manager)
            .Include(d => d.ParentDepartment)
            .Include(d => d.SubDepartments)
            .Include(d => d.Employees)
            .AsSplitQuery()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Department>> GetAllAsync()
    {
        Logger.Debug("Fetching all departments");
        return await _context.Departments
            .Include(d => d.Manager)
            .Include(d => d.ParentDepartment)
            .ToListAsync();
    }

    public async Task AddAsync(Department department)
    {
        Logger.Information("Adding department {DepartmentName} with parent {ParentId}", department.Name, department.ParentDepartmentId);
        if (department.ManagerId.HasValue)
        {
            department.Manager = await _context.Employees.FindAsync(department.ManagerId.Value);
        }
        if (department.ParentDepartmentId.HasValue)
        {
            department.ParentDepartment = await _context.Departments.FindAsync(department.ParentDepartmentId.Value);
        }
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        Logger.Information("Department {DepartmentName} added with id {DepartmentId}", department.Name, department.Id);
    }

    public async Task UpdateAsync(int id, Department department)
    {
        Logger.Information("Updating department {DepartmentId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Name = department.Name;
        existing.Description = department.Description;
        existing.ManagerId = department.ManagerId;
        existing.Manager = department.ManagerId.HasValue ? await _context.Employees.FindAsync(department.ManagerId.Value) : null;
        existing.ParentDepartmentId = department.ParentDepartmentId;
        existing.ParentDepartment = department.ParentDepartmentId.HasValue ? await _context.Departments.FindAsync(department.ParentDepartmentId.Value) : null;

        await _context.SaveChangesAsync();
        Logger.Information("Department {DepartmentId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting department {DepartmentId}", id);
        var department = await GetByIdAsync(id);
        if (department != null)
        {
            foreach (var sub in department.SubDepartments.ToList())
            {
                sub.ParentDepartmentId = null;
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            Logger.Information("Department {DepartmentId} deleted", id);
        }
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        Logger.Debug("Fetching employees for department view");
        return await _context.Employees.ToListAsync();
    }
}
