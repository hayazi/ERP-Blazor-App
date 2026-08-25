using ERPBlazorApp.Inventory.Models;
using ERPBlazorApp.RabbitMQ.Services;

namespace ERPBlazorApp.Inventory.Services;

public class InboundService
{
    private List<Inbound> _inbounds;
    private List<InboundDetail> _details;
    private List<Product> _products;
    private List<Supplier> _suppliers;
    private readonly EventPublisher _eventPublisher;

    public InboundService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _suppliers = InventorySampleData.GetSuppliers();
        _products = InventorySampleData.GetProducts();
        _inbounds = InventorySampleData.GetInbounds();
        _details = InventorySampleData.GetInboundDetails();

        foreach (var inbound in _inbounds)
        {
            inbound.Supplier = _suppliers.FirstOrDefault(s => s.Id == inbound.SupplierId);
            inbound.Details = _details.Where(d => d.InboundId == inbound.Id).ToList();
            foreach (var detail in inbound.Details)
            {
                detail.Inbound = inbound;
                detail.Product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            }
        }
    }

    public List<Inbound> GetAll() => _inbounds;
    public Inbound? GetById(int id) => _inbounds.FirstOrDefault(i => i.Id == id);
    public List<InboundDetail> GetDetails(int inboundId) => _details.Where(d => d.InboundId == inboundId).ToList();
    public List<Product> GetProducts() => _products;
    public List<Supplier> GetSuppliers() => _suppliers;

    public void Add(Inbound inbound)
    {
        inbound.Id = _inbounds.Any() ? _inbounds.Max(i => i.Id) + 1 : 1;
        inbound.Supplier = _suppliers.FirstOrDefault(s => s.Id == inbound.SupplierId);
        _inbounds.Add(inbound);

        foreach (var detail in inbound.Details)
        {
            detail.Id = _details.Any() ? _details.Max(d => d.Id) + 1 : 1;
            detail.InboundId = inbound.Id;
            detail.Inbound = inbound;
            detail.Product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            _details.Add(detail);

            var product = _products.FirstOrDefault(p => p.Id == detail.ProductId);
            if (product != null)
            {
                product.CurrentStock += detail.Quantity;
            }
        }

        var supplierName = inbound.Supplier?.Name ?? string.Empty;
        _eventPublisher.PublishInboundCreatedAsync(inbound.Id, inbound.SupplierId, supplierName, inbound.Date, inbound.Status, inbound.Details.Count).Wait();
    }

    public void Update(int id, Inbound inbound)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Reference = inbound.Reference;
        existing.Date = inbound.Date;
        existing.SupplierId = inbound.SupplierId;
        existing.Supplier = _suppliers.FirstOrDefault(s => s.Id == inbound.SupplierId);
        existing.Status = inbound.Status;
        existing.Notes = inbound.Notes;

        _details.RemoveAll(d => d.InboundId == id);
        existing.Details = inbound.Details.Select(d => new InboundDetail
        {
            Id = d.Id == 0 ? (_details.Any() ? _details.Max(dd => dd.Id) + 1 : 1) : d.Id,
            InboundId = id,
            ProductId = d.ProductId,
            Quantity = d.Quantity,
            UnitPrice = d.UnitPrice,
            TotalPrice = d.Quantity * d.UnitPrice,
            Inbound = existing,
            Product = _products.FirstOrDefault(p => p.Id == d.ProductId)
        }).ToList();

        foreach (var detail in existing.Details)
        {
            _details.Add(detail);
        }
    }

    public void Delete(int id)
    {
        var inbound = GetById(id);
        if (inbound != null)
        {
            _details.RemoveAll(d => d.InboundId == id);
            _inbounds.Remove(inbound);
        }
    }
}
