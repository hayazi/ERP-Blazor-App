using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.CRM.Models;
using ERPBlazorApp.CRM.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.CRM.Services;

public class CampaignService
{
    private readonly CRMDbContext _context;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<CampaignService>();

    public CampaignService(CRMDbContext context, EventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<Campaign>> GetAllAsync()
    {
        Logger.Debug("Fetching all campaigns");
        return await _context.Campaigns.ToListAsync();
    }

    public async Task<Campaign?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching campaign by id {CampaignId}", id);
        return await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Campaign campaign)
    {
        Logger.Information("Adding campaign {CampaignName}", campaign.Name);
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishCampaignCreatedAsync(campaign.Id, campaign.Name, campaign.Type, campaign.Status, campaign.StartDate, campaign.Budget);
        Logger.Information("Campaign added with id {CampaignId}", campaign.Id);
    }

    public async Task UpdateAsync(int id, Campaign campaign)
    {
        Logger.Information("Updating campaign {CampaignId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Name = campaign.Name;
        existing.Type = campaign.Type;
        existing.Status = campaign.Status;
        existing.StartDate = campaign.StartDate;
        existing.EndDate = campaign.EndDate;
        existing.Budget = campaign.Budget;
        existing.LeadsGenerated = campaign.LeadsGenerated;
        existing.Conversions = campaign.Conversions;
        existing.Description = campaign.Description;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishCampaignUpdatedAsync(id, campaign.Name, campaign.Type, campaign.Status, campaign.StartDate, campaign.Budget);
        Logger.Information("Campaign {CampaignId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting campaign {CampaignId}", id);
        var campaign = await GetByIdAsync(id);
        if (campaign != null)
        {
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            await _eventPublisher.PublishCampaignDeletedAsync(id, campaign.Name);
            Logger.Information("Campaign {CampaignId} deleted", id);
        }
    }
}
