using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.CRM.Models;
using ERPBlazorApp.CRM.Data;
using ERPBlazorApp.Data;
using Serilog;

namespace ERPBlazorApp.CRM.Services;

public class LeadService
{
    private readonly CRMDbContext _context;
    private readonly CacheService _cache;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<LeadService>();

    public LeadService(CRMDbContext context, CacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<Lead>> GetAllAsync()
    {
        Logger.Debug("Fetching all leads");
        var cacheKey = "leads:all";
        var cached = await _cache.GetAsync<List<Lead>>(cacheKey);
        if (cached != null) return cached;

        var leads = await _context.Leads.ToListAsync();
        await _cache.SetAsync(cacheKey, leads);
        return leads;
    }

    public async Task<Lead?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching lead by id {LeadId}", id);
        return await _context.Leads.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task AddAsync(Lead lead)
    {
        Logger.Information("Adding lead {LeadEmail}", lead.Email);
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("leads:all");
        Logger.Information("Lead added with id {LeadId}", lead.Id);
    }

    public async Task UpdateAsync(int id, Lead lead)
    {
        Logger.Information("Updating lead {LeadId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.FirstName = lead.FirstName;
        existing.LastName = lead.LastName;
        existing.Email = lead.Email;
        existing.Phone = lead.Phone;
        existing.Company = lead.Company;
        existing.Status = lead.Status;
        existing.Source = lead.Source;
        existing.Notes = lead.Notes;
        existing.AssignedToUserId = lead.AssignedToUserId;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("leads:all");
        Logger.Information("Lead {LeadId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting lead {LeadId}", id);
        var lead = await GetByIdAsync(id);
        if (lead != null)
        {
            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync("leads:all");
            Logger.Information("Lead {LeadId} deleted", id);
        }
    }
}
