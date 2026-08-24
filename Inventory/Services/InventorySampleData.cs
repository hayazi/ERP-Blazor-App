using ERPBlazorApp.Inventory.Models;

namespace ERPBlazorApp.Inventory.Services;

public static class InventorySampleData
{
    public static List<Category> GetCategories()
    {
        return new List<Category>
        {
            new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and components" },
            new Category { Id = 2, Name = "Office Supplies", Description = "Office stationery and supplies" },
            new Category { Id = 3, Name = "Raw Materials", Description = "Raw materials for production" }
        };
    }

    public static List<Supplier> GetSuppliers()
    {
        return new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Tech Parts Co.", ContactName = "John Smith", Phone = "021-12345678", Email = "sales@techparts.ir" },
            new Supplier { Id = 2, Name = "Office Depot", ContactName = "Sarah Johnson", Phone = "021-87654321", Email = "info@officedepot.ir" },
            new Supplier { Id = 3, Name = "Raw Material Ltd.", ContactName = "Ali Rezaei", Phone = "021-11223344", Email = "ali@rawmat.ir" }
        };
    }

    public static List<Customer> GetCustomers()
    {
        return new List<Customer>
        {
            new Customer { Id = 1, Name = "ABC Corporation", ContactName = "Mike Brown", Phone = "021-55555555", Email = "mike@abc.com" },
            new Customer { Id = 2, Name = "XYZ Industries", ContactName = "Lisa Davis", Phone = "021-66666666", Email = "lisa@xyz.com" },
            new Customer { Id = 3, Name = "Local Shop", ContactName = "Omar Hassan", Phone = "021-77777777", Email = "omar@localshop.ir" }
        };
    }

    public static List<Product> GetProducts()
    {
        var categories = GetCategories();
        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", SKU = "LAP-001", Unit = "Piece", PurchasePrice = 15000000, SalePrice = 18000000, CategoryId = 1, CurrentStock = 10, MinStock = 2 },
            new Product { Id = 2, Name = "Mouse", SKU = "MOU-001", Unit = "Piece", PurchasePrice = 150000, SalePrice = 200000, CategoryId = 1, CurrentStock = 50, MinStock = 10 },
            new Product { Id = 3, Name = "Keyboard", SKU = "KEY-001", Unit = "Piece", PurchasePrice = 500000, SalePrice = 700000, CategoryId = 1, CurrentStock = 30, MinStock = 5 },
            new Product { Id = 4, Name = "Paper A4", SKU = "PAP-001", Unit = "Box", PurchasePrice = 80000, SalePrice = 100000, CategoryId = 2, CurrentStock = 100, MinStock = 20 },
            new Product { Id = 5, Name = "Pen", SKU = "PEN-001", Unit = "Piece", PurchasePrice = 5000, SalePrice = 8000, CategoryId = 2, CurrentStock = 200, MinStock = 50 }
        };
    }

    public static List<Inbound> GetInbounds()
    {
        return new List<Inbound>
        {
            new Inbound { Id = 1, Reference = "IN-001", Date = DateTime.Today.AddDays(-5), SupplierId = 1, Status = "Received", Notes = "Monthly order" },
            new Inbound { Id = 2, Reference = "IN-002", Date = DateTime.Today.AddDays(-2), SupplierId = 2, Status = "Pending", Notes = "Urgent order" }
        };
    }

    public static List<InboundDetail> GetInboundDetails()
    {
        return new List<InboundDetail>
        {
            new InboundDetail { Id = 1, InboundId = 1, ProductId = 1, Quantity = 5, UnitPrice = 15000000, TotalPrice = 75000000 },
            new InboundDetail { Id = 2, InboundId = 1, ProductId = 2, Quantity = 20, UnitPrice = 150000, TotalPrice = 3000000 },
            new InboundDetail { Id = 3, InboundId = 2, ProductId = 4, Quantity = 50, UnitPrice = 80000, TotalPrice = 4000000 }
        };
    }

    public static List<Outbound> GetOutbounds()
    {
        return new List<Outbound>
        {
            new Outbound { Id = 1, Reference = "OUT-001", Date = DateTime.Today.AddDays(-3), CustomerId = 1, Status = "Shipped", Notes = "Express delivery" },
            new Outbound { Id = 2, Reference = "OUT-002", Date = DateTime.Today.AddDays(-1), CustomerId = 3, Status = "Pending", Notes = "Standard delivery" }
        };
    }

    public static List<OutboundDetail> GetOutboundDetails()
    {
        return new List<OutboundDetail>
        {
            new OutboundDetail { Id = 1, OutboundId = 1, ProductId = 1, Quantity = 2, UnitPrice = 18000000, TotalPrice = 36000000 },
            new OutboundDetail { Id = 2, OutboundId = 1, ProductId = 3, Quantity = 5, UnitPrice = 700000, TotalPrice = 3500000 },
            new OutboundDetail { Id = 3, OutboundId = 2, ProductId = 5, Quantity = 20, UnitPrice = 8000, TotalPrice = 160000 }
        };
    }

    public static List<Kardex> GetKardex()
    {
        return new List<Kardex>
        {
            new Kardex { Id = 1, ProductId = 1, Date = DateTime.Today.AddDays(-5), Type = "Inbound", ReferenceId = 1, Quantity = 5, Balance = 5, Notes = "IN-001" },
            new Kardex { Id = 2, ProductId = 1, Date = DateTime.Today.AddDays(-3), Type = "Outbound", ReferenceId = 1, Quantity = 2, Balance = 3, Notes = "OUT-001" },
            new Kardex { Id = 3, ProductId = 2, Date = DateTime.Today.AddDays(-5), Type = "Inbound", ReferenceId = 1, Quantity = 20, Balance = 20, Notes = "IN-001" },
            new Kardex { Id = 4, ProductId = 4, Date = DateTime.Today.AddDays(-2), Type = "Inbound", ReferenceId = 2, Quantity = 50, Balance = 50, Notes = "IN-002" }
        };
    }
}
