using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.AAA.Models;
using ERPBlazorApp.AAA.Data;
using Serilog;

namespace ERPBlazorApp.AAA.Services;

public class AccountService
{
    private readonly AAADbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<AccountService>();

    public AccountService(AAADbContext context)
    {
        _context = context;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        Logger.Debug("Fetching all accounts");
        return await _context.Accounts
            .Include(a => a.ParentAccount)
            .ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching account by id {AccountId}", id);
        return await _context.Accounts
            .Include(a => a.ParentAccount)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Account account)
    {
        Logger.Information("Adding account {AccountCode}", account.Code);
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        Logger.Information("Account added with id {AccountId}", account.Id);
    }

    public async Task UpdateAsync(int id, Account account)
    {
        Logger.Information("Updating account {AccountId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Code = account.Code;
        existing.Name = account.Name;
        existing.Type = account.Type;
        existing.ParentAccountId = account.ParentAccountId;
        existing.IsActive = account.IsActive;

        await _context.SaveChangesAsync();
        Logger.Information("Account {AccountId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting account {AccountId}", id);
        var account = await GetByIdAsync(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            Logger.Information("Account {AccountId} deleted", id);
        }
    }
}
