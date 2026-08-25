namespace ERPBlazorApp.RabbitMQ.Events.HumanResource;

public class EmployeeCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class EmployeeUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }
}

public class EmployeeDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class DepartmentCreatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public int? ParentDepartmentId { get; set; }
}

public class DepartmentUpdatedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public int? ParentDepartmentId { get; set; }
}

public class DepartmentDeletedEvent : ERPBlazorApp.RabbitMQ.Events.BaseEvent
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}
