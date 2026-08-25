using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class FiscalYearService
{
    private readonly AccountingDbContext _context;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<FiscalYearService>();

    public FiscalYearService(AccountingDbContext context, EventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<FiscalYear>> GetAllAsync()
    {
        Logger.Debug("Fetching all fiscal years");
        return await _context.FiscalYears
            .Include(f => f.Periods)
            .ToListAsync();
    }

    public async Task<FiscalYear?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching fiscal year by id {FiscalYearId}", id);
        return await _context.FiscalYears
            .Include(f => f.Periods)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddAsync(FiscalYear fiscalYear)
    {
        Logger.Information("Adding fiscal year {FiscalYearName}", fiscalYear.Name);
        _context.FiscalYears.Add(fiscalYear);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishFiscalYearCreatedAsync(fiscalYear.Id, fiscalYear.Name, fiscalYear.StartDate, fiscalYear.EndDate, fiscalYear.IsActive);
        Logger.Information("Fiscal year added with id {FiscalYearId}", fiscalYear.Id);
    }

    public async Task UpdateAsync(int id, FiscalYear fiscalYear)
    {
        Logger.Information("Updating fiscal year {FiscalYearId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Name = fiscalYear.Name;
        existing.StartDate = fiscalYear.StartDate;
        existing.EndDate = fiscalYear.EndDate;
        existing.IsActive = fiscalYear.IsActive;
        existing.IsClosed = fiscalYear.IsClosed;

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishFiscalYearUpdatedAsync(id, fiscalYear.Name, fiscalYear.StartDate, fiscalYear.EndDate, fiscalYear.IsActive);
        Logger.Information("Fiscal year {FiscalYearId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting fiscal year {FiscalYearId}", id);
        var fiscalYear = await GetByIdAsync(id);
        if (fiscalYear != null)
        {
            _context.FiscalYears.Remove(fiscalYear);
            await _context.SaveChangesAsync();
            Logger.Information("Fiscal year {FiscalYearId} deleted", id);
        }
    }
}
