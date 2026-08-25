using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class JournalEntryService
{
    private readonly AccountingDbContext _context;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<JournalEntryService>();

    public JournalEntryService(AccountingDbContext context, EventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<JournalEntry>> GetAllAsync()
    {
        Logger.Debug("Fetching all journal entries");
        return await _context.JournalEntries
            .Include(j => j.JournalEntryLines)
            .ThenInclude(l => l.Account)
            .ToListAsync();
    }

    public async Task<JournalEntry?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching journal entry by id {JournalEntryId}", id);
        return await _context.JournalEntries
            .Include(j => j.JournalEntryLines)
            .ThenInclude(l => l.Account)
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<List<JournalEntryLine>> GetLinesAsync(int journalEntryId)
    {
        Logger.Debug("Fetching lines for journal entry {JournalEntryId}", journalEntryId);
        return await _context.JournalEntryLines
            .Include(l => l.Account)
            .Where(l => l.JournalEntryId == journalEntryId)
            .ToListAsync();
    }

    public async Task<List<ChartOfAccount>> GetAccountsAsync()
    {
        Logger.Debug("Fetching all accounts for journal entry");
        return await _context.ChartOfAccounts.ToListAsync();
    }

    public async Task AddAsync(JournalEntry journalEntry)
    {
        Logger.Information("Adding journal entry {Reference}", journalEntry.Reference);
        _context.JournalEntries.Add(journalEntry);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishJournalEntryCreatedAsync(journalEntry.Id, journalEntry.Reference, journalEntry.Date, journalEntry.Status, journalEntry.TotalDebit, journalEntry.TotalCredit);
        Logger.Information("Journal entry added with id {JournalEntryId}", journalEntry.Id);
    }

    public async Task UpdateAsync(int id, JournalEntry journalEntry)
    {
        Logger.Information("Updating journal entry {JournalEntryId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Reference = journalEntry.Reference;
        existing.Date = journalEntry.Date;
        existing.Description = journalEntry.Description;
        existing.Status = journalEntry.Status;

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishJournalEntryUpdatedAsync(id, journalEntry.Reference, journalEntry.Date, journalEntry.Status, journalEntry.TotalDebit, journalEntry.TotalCredit);
        Logger.Information("Journal entry {JournalEntryId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting journal entry {JournalEntryId}", id);
        var journalEntry = await GetByIdAsync(id);
        if (journalEntry != null)
        {
            _context.JournalEntries.Remove(journalEntry);
            await _context.SaveChangesAsync();
            Logger.Information("Journal entry {JournalEntryId} deleted", id);
        }
    }
}
