using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Sales.Models;
using ERPBlazorApp.Sales.Data;
using ERPBlazorApp.RabbitMQ.Services;
using Serilog;

namespace ERPBlazorApp.Sales.Services;

public class SaleService
{
    private readonly SaleDbContext _context;
    private readonly EventPublisher _eventPublisher;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<SaleService>();

    public SaleService(SaleDbContext context, EventPublisher eventPublisher)
    {
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        Logger.Debug("Fetching all sales");
        return await _context.Sales
            .Include(s => s.Items)
            .Include(s => s.Payments)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching sale by id {SaleId}", id);
        return await _context.Sales
            .Include(s => s.Items)
            .Include(s => s.Payments)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale?> GetByInvoiceNumberAsync(string invoiceNumber)
    {
        Logger.Debug("Fetching sale by invoice number {InvoiceNumber}", invoiceNumber);
        return await _context.Sales
            .Include(s => s.Items)
            .Include(s => s.Payments)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.InvoiceNumber == invoiceNumber);
    }

    public async Task AddAsync(Sale sale)
    {
        Logger.Information("Adding sale {InvoiceNumber}", sale.InvoiceNumber);
        sale.CreatedAt = DateTime.Now;
        sale.SaleDate = DateTime.Now;
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        await _eventPublisher.PublishSaleCreatedAsync(sale.Id, sale.InvoiceNumber, sale.SaleDate, sale.CustomerName ?? string.Empty, sale.SubTotal, sale.TaxAmount, sale.DiscountAmount, sale.TotalAmount, sale.Status, sale.Items.Count);
        Logger.Information("Sale added with id {SaleId}", sale.Id);
    }

    public async Task UpdateAsync(int id, Sale sale)
    {
        Logger.Information("Updating sale {SaleId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.InvoiceNumber = sale.InvoiceNumber;
        existing.SaleDate = sale.SaleDate;
        existing.CustomerId = sale.CustomerId;
        existing.CustomerName = sale.CustomerName;
        existing.CustomerPhone = sale.CustomerPhone;
        existing.CustomerEmail = sale.CustomerEmail;
        existing.CustomerAddress = sale.CustomerAddress;
        existing.SubTotal = sale.SubTotal;
        existing.TaxAmount = sale.TaxAmount;
        existing.DiscountAmount = sale.DiscountAmount;
        existing.TotalAmount = sale.TotalAmount;
        existing.Status = sale.Status;
        existing.Notes = sale.Notes;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        await _eventPublisher.PublishSaleUpdatedAsync(id, sale.InvoiceNumber, sale.SaleDate, sale.CustomerName ?? string.Empty, sale.SubTotal, sale.TaxAmount, sale.DiscountAmount, sale.TotalAmount, sale.Status);
        Logger.Information("Sale {SaleId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting sale {SaleId}", id);
        var sale = await GetByIdAsync(id);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            await _eventPublisher.PublishSaleDeletedAsync(id, sale.InvoiceNumber);
            Logger.Information("Sale {SaleId} deleted", id);
        }
    }
}
