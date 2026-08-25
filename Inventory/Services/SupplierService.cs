using ERPBlazorApp.Inventory.Models;
using ERPBlazorApp.RabbitMQ.Services;

namespace ERPBlazorApp.Inventory.Services;

public class SupplierService
{
    private List<Supplier> _suppliers;
    private readonly EventPublisher _eventPublisher;

    public SupplierService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _suppliers = InventorySampleData.GetSuppliers();
    }

    public List<Supplier> GetAll() => _suppliers;
    public Supplier? GetById(int id) => _suppliers.FirstOrDefault(s => s.Id == id);

    public void Add(Supplier supplier)
    {
        supplier.Id = _suppliers.Any() ? _suppliers.Max(s => s.Id) + 1 : 1;
        _suppliers.Add(supplier);
        _eventPublisher.PublishSupplierCreatedAsync(supplier.Id, supplier.Name, supplier.ContactName, supplier.Phone, supplier.Email).Wait();
    }

    public void Update(int id, Supplier supplier)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = supplier.Name;
        existing.ContactName = supplier.ContactName;
        existing.Phone = supplier.Phone;
        existing.Email = supplier.Email;
        existing.Address = supplier.Address;
        _eventPublisher.PublishSupplierUpdatedAsync(id, supplier.Name, supplier.ContactName, supplier.Phone, supplier.Email).Wait();
    }

    public void Delete(int id)
    {
        var supplier = GetById(id);
        if (supplier != null)
        {
            _suppliers.Remove(supplier);
            _eventPublisher.PublishSupplierDeletedAsync(id, supplier.Name).Wait();
        }
    }
}
