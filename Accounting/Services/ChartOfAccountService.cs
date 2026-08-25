using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class ChartOfAccountService
{
    private readonly AccountingDbContext _context;
    private readonly CacheService _cache;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<ChartOfAccountService>();

    public ChartOfAccountService(AccountingDbContext context, CacheService cache, EventPublisher eventPublisher)
    {
        _context = context;
        _cache = cache;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<ChartOfAccount>> GetAllAsync()
    {
        Logger.Debug("Fetching all chart of accounts");
        var cacheKey = "chartofaccounts:all";
        var cached = await _cache.GetAsync<List<ChartOfAccount>>(cacheKey);
        if (cached != null) return cached;

        var accounts = await _context.ChartOfAccounts
            .Include(c => c.ParentAccount)
            .Include(c => c.ChildAccounts)
            .ToListAsync();

        await _cache.SetAsync(cacheKey, accounts);
        return accounts;
    }

    public async Task<ChartOfAccount?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching chart of account by id {AccountId}", id);
        return await _context.ChartOfAccounts
            .Include(c => c.ParentAccount)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(ChartOfAccount account)
    {
        Logger.Information("Adding chart of account {AccountCode}", account.Code);
        _context.ChartOfAccounts.Add(account);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("chartofaccounts:all");
        await _eventPublisher.PublishChartOfAccountCreatedAsync(account.Id, account.Code, account.Name, account.Type, account.IsActive);
        Logger.Information("Chart of account added with id {AccountId}", account.Id);
    }

    public async Task UpdateAsync(int id, ChartOfAccount account)
    {
        Logger.Information("Updating chart of account {AccountId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Code = account.Code;
        existing.Name = account.Name;
        existing.Type = account.Type;
        existing.ParentAccountId = account.ParentAccountId;
        existing.IsActive = account.IsActive;
        existing.CurrentBalance = account.CurrentBalance;

        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("chartofaccounts:all");
        await _eventPublisher.PublishChartOfAccountUpdatedAsync(id, account.Code, account.Name, account.Type, account.IsActive);
        Logger.Information("Chart of account {AccountId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting chart of account {AccountId}", id);
        var account = await GetByIdAsync(id);
        if (account != null)
        {
            _context.ChartOfAccounts.Remove(account);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync("chartofaccounts:all");
            Logger.Information("Chart of account {AccountId} deleted", id);
        }
    }
}
