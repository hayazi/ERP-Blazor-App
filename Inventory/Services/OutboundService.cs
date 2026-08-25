using ERPBlazorApp.Inventory.Models;
using ERPBlazorApp.RabbitMQ.Services;

namespace ERPBlazorApp.Inventory.Services;

public class OutboundService
{
    private List<Outbound> _outbounds;
    private List<OutboundDetail> _details;
    private List<Product> _products;
    private List<Customer> _customers;
    private readonly EventPublisher _eventPublisher;

    public OutboundService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _customers = InventorySampleData.GetCustomers();
        _products = InventorySampleData.GetProducts();
        _outbounds = InventorySampleData.GetOutbounds();
        _details = InventorySampleData.GetOutboundDetails();

        foreach (var outbound in _outbounds)
        {
            outbound.Customer = _customers.FirstOrDefault(c => c.Id == outbound.CustomerId);
            outbound.Details = _details.Where(d => d.OutboundId == outbound.Id).ToList();
            foreach (var detail in outbound.Details)
            {
                detail.Outbound = outbound;
                detail.Product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            }
        }
    }

    public List<Outbound> GetAll() => _outbounds;
    public Outbound? GetById(int id) => _outbounds.FirstOrDefault(o => o.Id == id);
    public List<OutboundDetail> GetDetails(int outboundId) => _details.Where(d => d.OutboundId == outboundId).ToList();
    public List<Product> GetProducts() => _products;
    public List<Customer> GetCustomers() => _customers;

    public void Add(Outbound outbound)
    {
        outbound.Id = _outbounds.Any() ? _outbounds.Max(o => o.Id) + 1 : 1;
        outbound.Customer = _customers.FirstOrDefault(c => c.Id == outbound.CustomerId);
        _outbounds.Add(outbound);

        foreach (var detail in outbound.Details)
        {
            detail.Id = _details.Any() ? _details.Max(d => d.Id) + 1 : 1;
            detail.OutboundId = outbound.Id;
            detail.Outbound = outbound;
            detail.Product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            _details.Add(detail);

            var product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            if (product != null)
            {
                product.CurrentStock -= detail.Quantity;
            }
        }

        var customerName = outbound.Customer?.Name ?? string.Empty;
        _eventPublisher.PublishOutboundCreatedAsync(outbound.Id, outbound.CustomerId, customerName, outbound.Date, outbound.Status, outbound.Details.Count).Wait();
    }

    public void Update(int id, Outbound outbound)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Reference = outbound.Reference;
        existing.Date = outbound.Date;
        existing.CustomerId = outbound.CustomerId;
        existing.Customer = _customers.FirstOrDefault(c => c.Id == outbound.CustomerId);
        existing.Status = outbound.Status;
        existing.Notes = outbound.Notes;

        _details.RemoveAll(d => d.OutboundId == id);
        existing.Details = outbound.Details.Select(d => new OutboundDetail
        {
            Id = d.Id == 0 ? (_details.Any() ? _details.Max(dd => dd.Id) + 1 : 1) : d.Id,
            OutboundId = id,
            ProductId = d.ProductId,
            Quantity = d.Quantity,
            UnitPrice = d.UnitPrice,
            TotalPrice = d.Quantity * d.UnitPrice,
            Outbound = existing,
            Product = _products.FirstOrDefault(p => p.Id == d.ProductId)
        }).ToList();

        foreach (var detail in existing.Details)
        {
            _details.Add(detail);
        }
    }

    public void Delete(int id)
    {
        var outbound = GetById(id);
        if (outbound != null)
        {
            _details.RemoveAll(d => d.OutboundId == id);
            _outbounds.Remove(outbound);
        }
    }
}
