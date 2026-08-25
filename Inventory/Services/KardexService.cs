using ERPBlazorApp.Inventory.Models;
using ERPBlazorApp.RabbitMQ.Services;

namespace ERPBlazorApp.Inventory.Services;

public class KardexService
{
    private List<Kardex> _kardex;
    private List<Product> _products;
    private readonly EventPublisher _eventPublisher;

    public KardexService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _products = InventorySampleData.GetProducts();
        _kardex = InventorySampleData.GetKardex();
        foreach (var item in _kardex)
        {
            item.Product = _products.FirstOrDefault(p => p.Id == item.ProductId);
        }
    }

    public List<Kardex> GetAll() => _kardex;
    public List<Kardex> GetByProduct(int productId) => _kardex.Where(k => k.ProductId == productId).OrderBy(k => k.Date).ToList();
    public List<Product> GetProducts() => _products;
}
