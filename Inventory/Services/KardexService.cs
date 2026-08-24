using ERPBlazorApp.Inventory.Models;

namespace ERPBlazorApp.Inventory.Services;

public class KardexService
{
    private List<Kardex> _kardex;
    private List<Product> _products;

    public KardexService()
    {
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
