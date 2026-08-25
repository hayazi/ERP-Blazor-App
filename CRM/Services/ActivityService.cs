using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.CRM.Models;
using ERPBlazorApp.CRM.Data;
using Serilog;

namespace ERPBlazorApp.CRM.Services;

public class ActivityService
{
    private readonly CRMDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<ActivityService>();

    public ActivityService(CRMDbContext context)
    {
        _context = context;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        Logger.Debug("Fetching all activities");
        return await _context.Activities
            .Include(a => a.Lead)
            .Include(a => a.Opportunity)
            .ToListAsync();
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching activity by id {ActivityId}", id);
        return await _context.Activities
            .Include(a => a.Lead)
            .Include(a => a.Opportunity)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Activity activity)
    {
        Logger.Information("Adding activity {ActivitySubject}", activity.Subject);
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        Logger.Information("Activity added with id {ActivityId}", activity.Id);
    }

    public async Task UpdateAsync(int id, Activity activity)
    {
        Logger.Information("Updating activity {ActivityId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Type = activity.Type;
        existing.Subject = activity.Subject;
        existing.Description = activity.Description;
        existing.DueDate = activity.DueDate;
        existing.Status = activity.Status;
        existing.LeadId = activity.LeadId;
        existing.CustomerId = activity.CustomerId;
        existing.OpportunityId = activity.OpportunityId;
        existing.AssignedToUserId = activity.AssignedToUserId;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        Logger.Information("Activity {ActivityId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting activity {ActivityId}", id);
        var activity = await GetByIdAsync(id);
        if (activity != null)
        {
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            Logger.Information("Activity {ActivityId} deleted", id);
        }
    }
}
