using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Accounting.Models;
using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.Accounting.Services;

public class BudgetService
{
    private readonly AccountingDbContext _context;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<BudgetService>();

    public BudgetService(AccountingDbContext context, EventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<Budget>> GetAllAsync()
    {
        Logger.Debug("Fetching all budgets");
        return await _context.Budgets
            .Include(b => b.Account)
            .ToListAsync();
    }

    public async Task<Budget?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching budget by id {BudgetId}", id);
        return await _context.Budgets
            .Include(b => b.Account)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Budget budget)
    {
        Logger.Information("Adding budget for account {AccountId}", budget.AccountId);
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishBudgetCreatedAsync(budget.Id, budget.AccountId, budget.Amount, budget.FiscalYearId ?? 0, budget.AccountingPeriodId ?? 0);
        Logger.Information("Budget added with id {BudgetId}", budget.Id);
    }

    public async Task UpdateAsync(int id, Budget budget)
    {
        Logger.Information("Updating budget {BudgetId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.AccountId = budget.AccountId;
        existing.FiscalYearId = budget.FiscalYearId;
        existing.AccountingPeriodId = budget.AccountingPeriodId;
        existing.Amount = budget.Amount;
        existing.Type = budget.Type;

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishBudgetUpdatedAsync(id, budget.AccountId, budget.Amount, budget.FiscalYearId ?? 0, budget.AccountingPeriodId ?? 0);
        Logger.Information("Budget {BudgetId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting budget {BudgetId}", id);
        var budget = await GetByIdAsync(id);
        if (budget != null)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            Logger.Information("Budget {BudgetId} deleted", id);
        }
    }
}
