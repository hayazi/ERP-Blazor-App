using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class ChartOfAccountService
{
    private readonly AccountingDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<ChartOfAccountService>();

    public ChartOfAccountService(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChartOfAccount>> GetAllAsync()
    {
        Logger.Debug("Fetching all chart of accounts");
        return await _context.ChartOfAccounts
            .Include(c => c.ParentAccount)
            .Include(c => c.ChildAccounts)
            .ToListAsync();
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
            Logger.Information("Chart of account {AccountId} deleted", id);
        }
    }
}
