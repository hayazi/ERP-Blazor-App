using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.HumanResource.Models;
using ERPBlazorApp.HumanResource.Data;
using ERPBlazorApp.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.HumanResource.Services;

public class EmployeeService
{
    private readonly HumanResourceDbContext _context;
    private readonly CacheService _cache;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<EmployeeService>();

    public EmployeeService(HumanResourceDbContext context, CacheService cache, EventPublisher eventPublisher)
    {
        _context = context;
        _cache = cache;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        Logger.Debug("Fetching all employees");
        var cacheKey = "employees:all";
        var cached = await _cache.GetAsync<List<Employee>>(cacheKey);
        if (cached != null) return cached;

        var employees = await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, employees);
        return employees;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching employee by id {EmployeeId}", id);
        return await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Employee employee)
    {
        Logger.Information("Adding employee {EmployeeName}", $"{employee.FirstName} {employee.LastName}");
        employee.Department = await _context.Departments.FindAsync(employee.DepartmentId);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("employees:all");
        await _eventPublisher.PublishEmployeeCreatedAsync(employee.Id, employee.FirstName, employee.LastName, employee.Email, employee.Phone, employee.Position, employee.DepartmentId, employee.Salary, employee.HireDate);
        Logger.Information("Employee added with id {EmployeeId}", employee.Id);
    }

    public async Task UpdateAsync(int id, Employee employee)
    {
        Logger.Information("Updating employee {EmployeeId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.FirstName = employee.FirstName;
        existing.LastName = employee.LastName;
        existing.Email = employee.Email;
        existing.Phone = employee.Phone;
        existing.Position = employee.Position;
        existing.DepartmentId = employee.DepartmentId;
        existing.Department = await _context.Departments.FindAsync(employee.DepartmentId);
        existing.HireDate = employee.HireDate;
        existing.Salary = employee.Salary;
        existing.IsActive = employee.IsActive;

        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("employees:all");
        await _eventPublisher.PublishEmployeeUpdatedAsync(id, employee.FirstName, employee.LastName, employee.Email, employee.Phone, employee.Position, employee.DepartmentId, employee.Salary);
        Logger.Information("Employee {EmployeeId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting employee {EmployeeId}", id);
        var employee = await GetByIdAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync("employees:all");
            await _eventPublisher.PublishEmployeeDeletedAsync(id, employee.FirstName, employee.LastName);
            Logger.Information("Employee {EmployeeId} deleted", id);
        }
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        Logger.Debug("Fetching all departments for employee dropdown");
        return await _context.Departments.ToListAsync();
    }
}
