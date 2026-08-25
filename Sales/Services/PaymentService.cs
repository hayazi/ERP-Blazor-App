using Microsoft.EntityFrameworkCore;
using ERPBlazorApp.Sales.Models;
using ERPBlazorApp.Sales.Data;
using Serilog;

namespace ERPBlazorApp.Sales.Services;

public class PaymentService
{
    private readonly SaleDbContext _context;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<PaymentService>();

    public PaymentService(SaleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        Logger.Debug("Fetching all payments");
        return await _context.Payments.ToListAsync();
    }

    public async Task<List<Payment>> GetBySaleIdAsync(int saleId)
    {
        Logger.Debug("Fetching payments for sale {SaleId}", saleId);
        return await _context.Payments.Where(p => p.SaleId == saleId).ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        Logger.Debug("Fetching payment by id {PaymentId}", id);
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Payment payment)
    {
        Logger.Information("Adding payment for sale {SaleId}", payment.SaleId);
        payment.CreatedAt = DateTime.Now;
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        Logger.Information("Payment added with id {PaymentId}", payment.Id);
    }

    public async Task UpdateAsync(int id, Payment payment)
    {
        Logger.Information("Updating payment {PaymentId}", id);
        var existing = await GetByIdAsync(id);
        if (existing == null) return;

        existing.Amount = payment.Amount;
        existing.PaymentMethod = payment.PaymentMethod;
        existing.Status = payment.Status;
        existing.ReferenceNumber = payment.ReferenceNumber;
        existing.PaymentDate = payment.PaymentDate;
        existing.Notes = payment.Notes;

        await _context.SaveChangesAsync();
        Logger.Information("Payment {PaymentId} updated successfully", id);
    }

    public async Task DeleteAsync(int id)
    {
        Logger.Warning("Deleting payment {PaymentId}", id);
        var payment = await GetByIdAsync(id);
        if (payment != null)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            Logger.Information("Payment {PaymentId} deleted", id);
        }
    }
}
