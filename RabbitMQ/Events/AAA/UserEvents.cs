namespace ERPBlazorApp.RabbitMQ.Events.AAA;

public class UserCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class UserUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class UserDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class RoleCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class RoleUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class RoleDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
