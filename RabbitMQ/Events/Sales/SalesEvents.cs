namespace ERPBlazorApp.RabbitMQ.Events.Sales;

public class SaleCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SaleId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public int ItemsCount { get; set; }
}

public class SaleUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SaleId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class SaleDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int SaleId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
}

public class PaymentCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int PaymentId { get; set; }
    public int SaleId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
}

public class PaymentUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int PaymentId { get; set; }
    public int SaleId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
}

public class PaymentDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int PaymentId { get; set; }
    public int SaleId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
}
