using ERPBlazorApp.Inventory.Models;

namespace ERPBlazorApp.Inventory.Services;

public class CategoryService
{
    private List<Category> _categories;

    public CategoryService()
    {
        _categories = InventorySampleData.GetCategories();
    }

    public List<Category> GetAll() => _categories;
    public Category? GetById(int id) => _categories.FirstOrDefault(c => c.Id == id);

    public void Add(Category category)
    {
        category.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
        _categories.Add(category);
    }

    public void Update(int id, Category category)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Name = category.Name;
        existing.Description = category.Description;
    }

    public void Delete(int id)
    {
        var category = GetById(id);
        if (category != null)
        {
            _categories.Remove(category);
        }
    }
}
