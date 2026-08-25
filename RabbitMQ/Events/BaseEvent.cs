namespace ERPBlazorApp.RabbitMQ.Events;

public class BaseEvent
{
    public string EventId { get; set; } = Guid.NewGuid().ToString();
    public string EventType { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public object? Data { get; set; }
}
