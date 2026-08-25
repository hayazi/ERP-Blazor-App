namespace ERPBlazorApp.RabbitMQ.Events.Inventory;

public class ProductCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public int CurrentStock { get; set; }
    public int CategoryId { get; set; }
}

public class ProductUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public int CurrentStock { get; set; }
    public int CategoryId { get; set; }
}

public class ProductDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class InboundCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int InboundId { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public int DetailsCount { get; set; }
}

public class OutboundCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int OutboundId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public int DetailsCount { get; set; }
}

public class KardexRecordCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int KardexId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string MovementType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Balance { get; set; }
    public DateTime Date { get; set; }
}

public class SupplierCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class SupplierUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class SupplierDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = string.Empty;
}
