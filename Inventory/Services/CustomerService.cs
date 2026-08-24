using ERPBlazorApp.Inventory.Models;

namespace ERPBlazorApp.Inventory.Services;

public class CustomerService
{
    private List<Customer> _customers;

    public CustomerService()
    {
        _customers = InventorySampleData.GetCustomers();
    }

    public List<Customer> GetAll() => _customers;
    public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

    public void Add(Customer customer)
    {
        customer.Id = _customers.Any() ? _customers.Max(c => c.Id) + 1 : 1;
        _customers.Add(customer);
    }

    public void Update(int id, Customer customer)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = customer.Name;
        existing.ContactName = customer.ContactName;
        existing.Phone = customer.Phone;
        existing.Email = customer.Email;
        existing.Address = customer.Address;
    }

    public void Delete(int id)
    {
        var customer = GetById(id);
        if (customer != null)
        {
            _customers.Remove(customer);
        }
    }
}
