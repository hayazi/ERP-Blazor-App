using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class TrialBalanceService
{
    private readonly AccountingDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<TrialBalanceService>();

    public TrialBalanceService(AccountingDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrialBalance>> GetAllAsync()
    {
        Logger.Debug("Fetching trial balance");
        return await _context.TrialBalances
            .Include(t => t.Account)
            .ToListAsync();
    }
}
