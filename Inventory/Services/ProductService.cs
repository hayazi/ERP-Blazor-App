using ERPBlazorApp.Inventory.Models;
using ERPBlazorApp.RabbitMQ.Services;

namespace ERPBlazorApp.Inventory.Services;

public class ProductService
{
    private List<Product> _products;
    private List<Category> _categories;
    private readonly EventPublisher _eventPublisher;

    public ProductService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _categories = InventorySampleData.GetCategories();
        _products = InventorySampleData.GetProducts();
        _products.ForEach(p => p.Category = _categories.FirstOrDefault(c => c.Id == p.CategoryId));
    }

    public List<Product> GetAll() => _products;
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    public List<Category> GetCategories() => _categories;

    public void Add(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        product.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
        _products.Add(product);
        _eventPublisher.PublishProductCreatedAsync(product.Id, product.Name, product.SKU, product.PurchasePrice, product.SalePrice, product.CurrentStock, product.CategoryId).Wait();
    }

    public void Update(int id, Product product)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.SKU = product.SKU;
        existing.Unit = product.Unit;
        existing.PurchasePrice = product.PurchasePrice;
        existing.SalePrice = product.SalePrice;
        existing.CategoryId = product.CategoryId;
        existing.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
        existing.CurrentStock = product.CurrentStock;
        existing.MinStock = product.MinStock;
        existing.IsActive = product.IsActive;
        _eventPublisher.PublishProductUpdatedAsync(id, product.Name, product.SKU, product.PurchasePrice, product.SalePrice, product.CurrentStock, product.CategoryId).Wait();
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _products.Remove(product);
            _eventPublisher.PublishProductDeletedAsync(id, product.Name).Wait();
        }
    }
}
