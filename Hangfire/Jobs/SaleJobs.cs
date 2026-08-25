using Serilog;
using ERPBlazorApp.Sales.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPBlazorApp.Hangfire.Jobs;

public class SaleJobs
{
    private readonly SaleDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<SaleJobs>();

    public SaleJobs(SaleDbContext context)
    {
        _context = context;
    }

    public async Task GenerateDailySalesReport()
    {
        Logger.Information("Starting daily sales report generation");
        
        var today = DateTime.Today;
        var sales = await _context.Sales
            .Where(s => s.SaleDate.Date == today)
            .Include(s => s.Items)
            .Include(s => s.Payments)
            .AsSplitQuery()
            .ToListAsync();

        var totalSales = sales.Count;
        var totalRevenue = sales.Sum(s => s.TotalAmount);
        var completedSales = sales.Count(s => s.Status == "Completed");

        Logger.Information("Daily Sales Report - Date: {Date}, Total Sales: {TotalSales}, Revenue: {Revenue:C}, Completed: {Completed}",
            today, totalSales, totalRevenue, completedSales);
    }
}
