namespace ERPBlazorApp.RabbitMQ.Events.CRM;

public class LeadCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int LeadId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
}

public class LeadUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int LeadId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
}

public class LeadDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int LeadId { get; set; }
    public string Email { get; set; } = string.Empty;
}

public class CustomerCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class CustomerUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class CustomerDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class OpportunityCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int OpportunityId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal EstimatedValue { get; set; }
    public string Stage { get; set; } = string.Empty;
    public string Probability { get; set; } = string.Empty;
}

public class OpportunityUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int OpportunityId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal EstimatedValue { get; set; }
    public string Stage { get; set; } = string.Empty;
    public string Probability { get; set; } = string.Empty;
}

public class OpportunityDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int OpportunityId { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class ActivityCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ActivityId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ActivityUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ActivityId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ActivityDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int ActivityId { get; set; }
    public string Subject { get; set; } = string.Empty;
}

public class CampaignCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Budget { get; set; }
}

public class CampaignUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Budget { get; set; }
}

public class CampaignDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int CampaignId { get; set; }
    public string Name { get; set; } = string.Empty;
}
