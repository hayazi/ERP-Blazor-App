using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.CRM.Models;
using ERPBlazorApp.CRM.Data;
using Serilog;

namespace ERPBlazorApp.CRM.Services;

public class OpportunityService
{
    private readonly CRMDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<OpportunityService>();

    public OpportunityService(CRMDbContext context)
    {
        _context = context;
    }

    public async Task<List<Opportunity>> GetAllAsync()
    {
        Logger.Debug("Fetching all opportunities");
        return await _context.Opportunities
            .Include(o => o.Lead)
            .ToListAsync();
    }

    public async Task<Opportunity?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching opportunity by id {OpportunityId}", id);
        return await _context.Opportunities
            .Include(o => o.Lead)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Opportunity opportunity)
    {
        Logger.Information("Adding opportunity {OpportunityTitle}", opportunity.Title);
        _context.Opportunities.Add(opportunity);
        await _context.SaveChangesAsync();
        Logger.Information("Opportunity added with id {OpportunityId}", opportunity.Id);
    }

    public async Task UpdateAsync(int id, Opportunity opportunity)
    {
        Logger.Information("Updating opportunity {OpportunityId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Title = opportunity.Title;
        existing.Description = opportunity.Description;
        existing.EstimatedValue = opportunity.EstimatedValue;
        existing.Stage = opportunity.Stage;
        existing.Probability = opportunity.Probability;
        existing.ExpectedCloseDate = opportunity.ExpectedCloseDate;
        existing.LeadId = opportunity.LeadId;
        existing.CustomerId = opportunity.CustomerId;
        existing.AssignedToUserId = opportunity.AssignedToUserId;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        Logger.Information("Opportunity {OpportunityId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting opportunity {OpportunityId}", id);
        var opportunity = await GetByIdAsync(id);
        if (opportunity != null)
        {
            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            Logger.Information("Opportunity {OpportunityId} deleted", id);
        }
    }
}
