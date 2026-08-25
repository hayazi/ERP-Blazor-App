using Serilog;
using ERPBlazorApp.Inventory.Data;
using ERPBlazorApp.Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPBlazorApp.Hangfire.Jobs;

public class InventoryJobs
{
    private readonly InventoryDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<InventoryJobs>();

    public InventoryJobs(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task CleanupOldKardexRecords()
    {
        Logger.Information("Starting cleanup of old kardex records");
        
        var cutoffDate = DateTime.Now.AddYears(-2);
        var oldRecords = await _context.Kardex
            .Where(k => k.Date < cutoffDate)
            .ToListAsync();

        if (oldRecords.Any())
        {
            _context.Kardex.RemoveRange(oldRecords);
            await _context.SaveChangesAsync();
            Logger.Information("Cleaned up {Count} old kardex records", oldRecords.Count);
        }
        else
        {
            Logger.Information("No old kardex records found to clean up");
        }
    }
}
