using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Sales.Models;
using ERPBlazorApp.Sales.Data;
using Serilog;

namespace ERPBlazorApp.Sales.Services;

public class SaleItemService
{
    private readonly SaleDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<SaleItemService>();

    public SaleItemService(SaleDbContext context)
    {
        _context = context;
    }

    public async Task<List<SaleItem>> GetAllAsync()
    {
        Logger.Debug("Fetching all sale items");
        return await _context.SaleItems.ToListAsync();
    }

    public async Task<List<SaleItem>> GetBySaleIdAsync(int saleId)
    {
        Logger.Debug("Fetching sale items for sale {SaleId}", saleId);
        return await _context.SaleItems.Where(si => si.SaleId == saleId).ToListAsync();
    }

    public async Task<SaleItem?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching sale item by id {SaleItemId}", id);
        return await _context.SaleItems.FirstOrDefaultAsync(si => si.Id == id);
    }

    public async Task AddAsync(SaleItem saleItem)
    {
        Logger.Information("Adding sale item {ProductName}", saleItem.ProductName);
        _context.SaleItems.Add(saleItem);
        await _context.SaveChangesAsync();
        Logger.Information("Sale item added with id {SaleItemId}", saleItem.Id);
    }

    public async Task UpdateAsync(int id, SaleItem saleItem)
    {
        Logger.Information("Updating sale item {SaleItemId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.ProductId = saleItem.ProductId;
        existing.ProductName = saleItem.ProductName;
        existing.ProductSku = saleItem.ProductSku;
        existing.Quantity = saleItem.Quantity;
        existing.UnitPrice = saleItem.UnitPrice;
        existing.TotalPrice = saleItem.TotalPrice;
        existing.DiscountAmount = saleItem.DiscountAmount;
        existing.Notes = saleItem.Notes;

        await _context.SaveChangesAsync();
        Logger.Information("Sale item {SaleItemId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting sale item {SaleItemId}", id);
        var saleItem = await GetByIdAsync(id);
        if (saleItem != null)
        {
            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync();
            Logger.Information("Sale item {SaleItemId} deleted", id);
        }
    }
}
