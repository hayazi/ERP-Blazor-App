namespace ERPBlazorApp.RabbitMQ.Events.Accounting;

public class ChartOfAccountCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int AccountId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class ChartOfAccountUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int AccountId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class JournalEntryCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int JournalEntryId { get; set; }
    public string EntryNumber { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
}

public class JournalEntryUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int JournalEntryId { get; set; }
    public string EntryNumber { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
}

public class FiscalYearCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int FiscalYearId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}

public class FiscalYearUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int FiscalYearId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}

public class BudgetCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int BudgetId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public int FiscalYearId { get; set; }
    public int AccountingPeriodId { get; set; }
}

public class BudgetUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int BudgetId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public int FiscalYearId { get; set; }
    public int AccountingPeriodId { get; set; }
}
