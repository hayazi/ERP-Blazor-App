using ERPBlazorApp.RabbitMQ.Events;
using ERPBlazorApp.RabbitMQ.Events.AAA;
using ERPBlazorApp.RabbitMQ.Events.Accounting;
using ERPBlazorApp.RabbitMQ.Events.CRM;
using ERPBlazorApp.RabbitMQ.Events.HumanResource;
using ERPBlazorApp.RabbitMQ.Events.Inventory;
using ERPBlazorApp.RabbitMQ.Events.Sales;
using Serilog;

namespace ERPBlazorApp.RabbitMQ.Services;

public class EventPublisher
{
    private readonly RabbitMQService _rabbitMQ;
    private readonly Serilog.ILogger _logger = Log.ForContext<EventPublisher>();

    public EventPublisher(RabbitMQService rabbitMQ)
    {
        _rabbitMQ = rabbitMQ;
    }

    public Task PublishAsync<T>(string routingKey, T eventData) where T : BaseEvent
    {
        eventData.EventId = Guid.NewGuid().ToString();
        eventData.OccurredAt = DateTime.UtcNow;
        return _rabbitMQ.PublishAsync(routingKey, eventData);
    }

    #region AAA Events

    public Task PublishUserCreatedAsync(int userId, string username, string email, bool isActive)
    {
        var evt = new UserCreatedEvent
        {
            Module = "AAA",
            EventType = "UserCreated",
            UserId = userId,
            Username = username,
            Email = email,
            IsActive = isActive
        };
        return PublishAsync("aaa.user.created", evt);
    }

    public Task PublishUserUpdatedAsync(int userId, string username, string email, bool isActive)
    {
        var evt = new UserUpdatedEvent
        {
            Module = "AAA",
            EventType = "UserUpdated",
            UserId = userId,
            Username = username,
            Email = email,
            IsActive = isActive
        };
        return PublishAsync("aaa.user.updated", evt);
    }

    public Task PublishUserDeletedAsync(int userId, string username)
    {
        var evt = new UserDeletedEvent
        {
            Module = "AAA",
            EventType = "UserDeleted",
            UserId = userId,
            Username = username
        };
        return PublishAsync("aaa.user.deleted", evt);
    }

    public Task PublishRoleCreatedAsync(int roleId, string roleName, string description)
    {
        var evt = new RoleCreatedEvent
        {
            Module = "AAA",
            EventType = "RoleCreated",
            RoleId = roleId,
            RoleName = roleName,
            Description = description
        };
        return PublishAsync("aaa.role.created", evt);
    }

    public Task PublishRoleUpdatedAsync(int roleId, string roleName, string description)
    {
        var evt = new RoleUpdatedEvent
        {
            Module = "AAA",
            EventType = "RoleUpdated",
            RoleId = roleId,
            RoleName = roleName,
            Description = description
        };
        return PublishAsync("aaa.role.updated", evt);
    }

    public Task PublishRoleDeletedAsync(int roleId, string roleName)
    {
        var evt = new RoleDeletedEvent
        {
            Module = "AAA",
            EventType = "RoleDeleted",
            RoleId = roleId,
            RoleName = roleName
        };
        return PublishAsync("aaa.role.deleted", evt);
    }

    #endregion

    #region Accounting Events

    public Task PublishChartOfAccountCreatedAsync(int accountId, string code, string name, string type, bool isActive)
    {
        var evt = new ChartOfAccountCreatedEvent
        {
            Module = "Accounting",
            EventType = "ChartOfAccountCreated",
            AccountId = accountId,
            Code = code,
            Name = name,
            Type = type,
            IsActive = isActive
        };
        return PublishAsync("accounting.chartofaccount.created", evt);
    }

    public Task PublishChartOfAccountUpdatedAsync(int accountId, string code, string name, string type, bool isActive)
    {
        var evt = new ChartOfAccountUpdatedEvent
        {
            Module = "Accounting",
            EventType = "ChartOfAccountUpdated",
            AccountId = accountId,
            Code = code,
            Name = name,
            Type = type,
            IsActive = isActive
        };
        return PublishAsync("accounting.chartofaccount.updated", evt);
    }

    public Task PublishJournalEntryCreatedAsync(int journalEntryId, string entryNumber, DateTime entryDate, string status, decimal totalDebit, decimal totalCredit)
    {
        var evt = new JournalEntryCreatedEvent
        {
            Module = "Accounting",
            EventType = "JournalEntryCreated",
            JournalEntryId = journalEntryId,
            EntryNumber = entryNumber,
            EntryDate = entryDate,
            Status = status,
            TotalDebit = totalDebit,
            TotalCredit = totalCredit
        };
        return PublishAsync("accounting.journalentry.created", evt);
    }

    public Task PublishJournalEntryUpdatedAsync(int journalEntryId, string entryNumber, DateTime entryDate, string status, decimal totalDebit, decimal totalCredit)
    {
        var evt = new JournalEntryUpdatedEvent
        {
            Module = "Accounting",
            EventType = "JournalEntryUpdated",
            JournalEntryId = journalEntryId,
            EntryNumber = entryNumber,
            EntryDate = entryDate,
            Status = status,
            TotalDebit = totalDebit,
            TotalCredit = totalCredit
        };
        return PublishAsync("accounting.journalentry.updated", evt);
    }

    public Task PublishFiscalYearCreatedAsync(int fiscalYearId, string name, DateTime startDate, DateTime endDate, bool isActive)
    {
        var evt = new FiscalYearCreatedEvent
        {
            Module = "Accounting",
            EventType = "FiscalYearCreated",
            FiscalYearId = fiscalYearId,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = isActive
        };
        return PublishAsync("accounting.fiscalyear.created", evt);
    }

    public Task PublishFiscalYearUpdatedAsync(int fiscalYearId, string name, DateTime startDate, DateTime endDate, bool isActive)
    {
        var evt = new FiscalYearUpdatedEvent
        {
            Module = "Accounting",
            EventType = "FiscalYearUpdated",
            FiscalYearId = fiscalYearId,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = isActive
        };
        return PublishAsync("accounting.fiscalyear.updated", evt);
    }

    public Task PublishBudgetCreatedAsync(int budgetId, int accountId, decimal amount, int fiscalYearId, int accountingPeriodId)
    {
        var evt = new BudgetCreatedEvent
        {
            Module = "Accounting",
            EventType = "BudgetCreated",
            BudgetId = budgetId,
            AccountId = accountId,
            Amount = amount,
            FiscalYearId = fiscalYearId,
            AccountingPeriodId = accountingPeriodId
        };
        return PublishAsync("accounting.budget.created", evt);
    }

    public Task PublishBudgetUpdatedAsync(int budgetId, int accountId, decimal amount, int fiscalYearId, int accountingPeriodId)
    {
        var evt = new BudgetUpdatedEvent
        {
            Module = "Accounting",
            EventType = "BudgetUpdated",
            BudgetId = budgetId,
            AccountId = accountId,
            Amount = amount,
            FiscalYearId = fiscalYearId,
            AccountingPeriodId = accountingPeriodId
        };
        return PublishAsync("accounting.budget.updated", evt);
    }

    #endregion

    #region CRM Events

    public Task PublishLeadCreatedAsync(int leadId, string firstName, string lastName, string email, string phone, string company, string status, string source)
    {
        var evt = new LeadCreatedEvent
        {
            Module = "CRM",
            EventType = "LeadCreated",
            LeadId = leadId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Company = company,
            Status = status,
            Source = source
        };
        return PublishAsync("crm.lead.created", evt);
    }

    public Task PublishLeadUpdatedAsync(int leadId, string firstName, string lastName, string email, string phone, string company, string status, string source)
    {
        var evt = new LeadUpdatedEvent
        {
            Module = "CRM",
            EventType = "LeadUpdated",
            LeadId = leadId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Company = company,
            Status = status,
            Source = source
        };
        return PublishAsync("crm.lead.updated", evt);
    }

    public Task PublishLeadDeletedAsync(int leadId, string email)
    {
        var evt = new LeadDeletedEvent
        {
            Module = "CRM",
            EventType = "LeadDeleted",
            LeadId = leadId,
            Email = email
        };
        return PublishAsync("crm.lead.deleted", evt);
    }

    public Task PublishCustomerCreatedAsync(int customerId, string name, string contactName, string phone, string email, string address)
    {
        var evt = new CustomerCreatedEvent
        {
            Module = "CRM",
            EventType = "CustomerCreated",
            CustomerId = customerId,
            Name = name,
            ContactName = contactName,
            Phone = phone,
            Email = email,
            Address = address
        };
        return PublishAsync("crm.customer.created", evt);
    }

    public Task PublishCustomerUpdatedAsync(int customerId, string name, string contactName, string phone, string email, string address)
    {
        var evt = new CustomerUpdatedEvent
        {
            Module = "CRM",
            EventType = "CustomerUpdated",
            CustomerId = customerId,
            Name = name,
            ContactName = contactName,
            Phone = phone,
            Email = email,
            Address = address
        };
        return PublishAsync("crm.customer.updated", evt);
    }

    public Task PublishCustomerDeletedAsync(int customerId, string name)
    {
        var evt = new CustomerDeletedEvent
        {
            Module = "CRM",
            EventType = "CustomerDeleted",
            CustomerId = customerId,
            Name = name
        };
        return PublishAsync("crm.customer.deleted", evt);
    }

    public Task PublishOpportunityCreatedAsync(int opportunityId, string title, decimal estimatedValue, string stage, string probability)
    {
        var evt = new OpportunityCreatedEvent
        {
            Module = "CRM",
            EventType = "OpportunityCreated",
            OpportunityId = opportunityId,
            Title = title,
            EstimatedValue = estimatedValue,
            Stage = stage,
            Probability = probability
        };
        return PublishAsync("crm.opportunity.created", evt);
    }

    public Task PublishOpportunityUpdatedAsync(int opportunityId, string title, decimal estimatedValue, string stage, string probability)
    {
        var evt = new OpportunityUpdatedEvent
        {
            Module = "CRM",
            EventType = "OpportunityUpdated",
            OpportunityId = opportunityId,
            Title = title,
            EstimatedValue = estimatedValue,
            Stage = stage,
            Probability = probability
        };
        return PublishAsync("crm.opportunity.updated", evt);
    }

    public Task PublishOpportunityDeletedAsync(int opportunityId, string title)
    {
        var evt = new OpportunityDeletedEvent
        {
            Module = "CRM",
            EventType = "OpportunityDeleted",
            OpportunityId = opportunityId,
            Title = title
        };
        return PublishAsync("crm.opportunity.deleted", evt);
    }

    public Task PublishActivityCreatedAsync(int activityId, string type, string subject, DateTime dueDate, string status)
    {
        var evt = new ActivityCreatedEvent
        {
            Module = "CRM",
            EventType = "ActivityCreated",
            ActivityId = activityId,
            Type = type,
            Subject = subject,
            DueDate = dueDate,
            Status = status
        };
        return PublishAsync("crm.activity.created", evt);
    }

    public Task PublishActivityUpdatedAsync(int activityId, string type, string subject, DateTime dueDate, string status)
    {
        var evt = new ActivityUpdatedEvent
        {
            Module = "CRM",
            EventType = "ActivityUpdated",
            ActivityId = activityId,
            Type = type,
            Subject = subject,
            DueDate = dueDate,
            Status = status
        };
        return PublishAsync("crm.activity.updated", evt);
    }

    public Task PublishActivityDeletedAsync(int activityId, string subject)
    {
        var evt = new ActivityDeletedEvent
        {
            Module = "CRM",
            EventType = "ActivityDeleted",
            ActivityId = activityId,
            Subject = subject
        };
        return PublishAsync("crm.activity.deleted", evt);
    }

    public Task PublishCampaignCreatedAsync(int campaignId, string name, string type, string status, DateTime startDate, decimal budget)
    {
        var evt = new CampaignCreatedEvent
        {
            Module = "CRM",
            EventType = "CampaignCreated",
            CampaignId = campaignId,
            Name = name,
            Type = type,
            Status = status,
            StartDate = startDate,
            Budget = budget
        };
        return PublishAsync("crm.campaign.created", evt);
    }

    public Task PublishCampaignUpdatedAsync(int campaignId, string name, string type, string status, DateTime startDate, decimal budget)
    {
        var evt = new CampaignUpdatedEvent
        {
            Module = "CRM",
            EventType = "CampaignUpdated",
            CampaignId = campaignId,
            Name = name,
            Type = type,
            Status = status,
            StartDate = startDate,
            Budget = budget
        };
        return PublishAsync("crm.campaign.updated", evt);
    }

    public Task PublishCampaignDeletedAsync(int campaignId, string name)
    {
        var evt = new CampaignDeletedEvent
        {
            Module = "CRM",
            EventType = "CampaignDeleted",
            CampaignId = campaignId,
            Name = name
        };
        return PublishAsync("crm.campaign.deleted", evt);
    }

    #endregion

    #region HumanResource Events

    public Task PublishEmployeeCreatedAsync(int employeeId, string firstName, string lastName, string email, string phone, string position, int departmentId, decimal salary, DateTime hireDate)
    {
        var evt = new EmployeeCreatedEvent
        {
            Module = "HumanResource",
            EventType = "EmployeeCreated",
            EmployeeId = employeeId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Position = position,
            DepartmentId = departmentId,
            Salary = salary,
            HireDate = hireDate
        };
        return PublishAsync("hr.employee.created", evt);
    }

    public Task PublishEmployeeUpdatedAsync(int employeeId, string firstName, string lastName, string email, string phone, string position, int departmentId, decimal salary)
    {
        var evt = new EmployeeUpdatedEvent
        {
            Module = "HumanResource",
            EventType = "EmployeeUpdated",
            EmployeeId = employeeId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Position = position,
            DepartmentId = departmentId,
            Salary = salary
        };
        return PublishAsync("hr.employee.updated", evt);
    }

    public Task PublishEmployeeDeletedAsync(int employeeId, string firstName, string lastName)
    {
        var evt = new EmployeeDeletedEvent
        {
            Module = "HumanResource",
            EventType = "EmployeeDeleted",
            EmployeeId = employeeId,
            FirstName = firstName,
            LastName = lastName
        };
        return PublishAsync("hr.employee.deleted", evt);
    }

    public Task PublishDepartmentCreatedAsync(int departmentId, string name, string description, int? managerId, int? parentDepartmentId)
    {
        var evt = new DepartmentCreatedEvent
        {
            Module = "HumanResource",
            EventType = "DepartmentCreated",
            DepartmentId = departmentId,
            Name = name,
            Description = description,
            ManagerId = managerId,
            ParentDepartmentId = parentDepartmentId
        };
        return PublishAsync("hr.department.created", evt);
    }

    public Task PublishDepartmentUpdatedAsync(int departmentId, string name, string description, int? managerId, int? parentDepartmentId)
    {
        var evt = new DepartmentUpdatedEvent
        {
            Module = "HumanResource",
            EventType = "DepartmentUpdated",
            DepartmentId = departmentId,
            Name = name,
            Description = description,
            ManagerId = managerId,
            ParentDepartmentId = parentDepartmentId
        };
        return PublishAsync("hr.department.updated", evt);
    }

    public Task PublishDepartmentDeletedAsync(int departmentId, string name)
    {
        var evt = new DepartmentDeletedEvent
        {
            Module = "HumanResource",
            EventType = "DepartmentDeleted",
            DepartmentId = departmentId,
            Name = name
        };
        return PublishAsync("hr.department.deleted", evt);
    }

    #endregion

    #region Inventory Events

    public Task PublishProductCreatedAsync(int productId, string name, string sku, decimal purchasePrice, decimal salePrice, int currentStock, int categoryId)
    {
        var evt = new ProductCreatedEvent
        {
            Module = "Inventory",
            EventType = "ProductCreated",
            ProductId = productId,
            Name = name,
            SKU = sku,
            PurchasePrice = purchasePrice,
            SalePrice = salePrice,
            CurrentStock = currentStock,
            CategoryId = categoryId
        };
        return PublishAsync("inventory.product.created", evt);
    }

    public Task PublishProductUpdatedAsync(int productId, string name, string sku, decimal purchasePrice, decimal salePrice, int currentStock, int categoryId)
    {
        var evt = new ProductUpdatedEvent
        {
            Module = "Inventory",
            EventType = "ProductUpdated",
            ProductId = productId,
            Name = name,
            SKU = sku,
            PurchasePrice = purchasePrice,
            SalePrice = salePrice,
            CurrentStock = currentStock,
            CategoryId = categoryId
        };
        return PublishAsync("inventory.product.updated", evt);
    }

    public Task PublishProductDeletedAsync(int productId, string name)
    {
        var evt = new ProductDeletedEvent
        {
            Module = "Inventory",
            EventType = "ProductDeleted",
            ProductId = productId,
            Name = name
        };
        return PublishAsync("inventory.product.deleted", evt);
    }

    public Task PublishInboundCreatedAsync(int inboundId, int supplierId, string supplierName, DateTime date, string status, int detailsCount)
    {
        var evt = new InboundCreatedEvent
        {
            Module = "Inventory",
            EventType = "InboundCreated",
            InboundId = inboundId,
            SupplierId = supplierId,
            SupplierName = supplierName,
            Date = date,
            Status = status,
            DetailsCount = detailsCount
        };
        return PublishAsync("inventory.inbound.created", evt);
    }

    public Task PublishOutboundCreatedAsync(int outboundId, int customerId, string customerName, DateTime date, string status, int detailsCount)
    {
        var evt = new OutboundCreatedEvent
        {
            Module = "Inventory",
            EventType = "OutboundCreated",
            OutboundId = outboundId,
            CustomerId = customerId,
            CustomerName = customerName,
            Date = date,
            Status = status,
            DetailsCount = detailsCount
        };
        return PublishAsync("inventory.outbound.created", evt);
    }

    public Task PublishKardexRecordCreatedAsync(int kardexId, int productId, string productName, string movementType, int quantity, decimal unitPrice, int balance, DateTime date)
    {
        var evt = new KardexRecordCreatedEvent
        {
            Module = "Inventory",
            EventType = "KardexRecordCreated",
            KardexId = kardexId,
            ProductId = productId,
            ProductName = productName,
            MovementType = movementType,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Balance = balance,
            Date = date
        };
        return PublishAsync("inventory.kardex.created", evt);
    }

    public Task PublishSupplierCreatedAsync(int supplierId, string name, string contactName, string phone, string email)
    {
        var evt = new SupplierCreatedEvent
        {
            Module = "Inventory",
            EventType = "SupplierCreated",
            SupplierId = supplierId,
            Name = name,
            ContactName = contactName,
            Phone = phone,
            Email = email
        };
        return PublishAsync("inventory.supplier.created", evt);
    }

    public Task PublishSupplierUpdatedAsync(int supplierId, string name, string contactName, string phone, string email)
    {
        var evt = new SupplierUpdatedEvent
        {
            Module = "Inventory",
            EventType = "SupplierUpdated",
            SupplierId = supplierId,
            Name = name,
            ContactName = contactName,
            Phone = phone,
            Email = email
        };
        return PublishAsync("inventory.supplier.updated", evt);
    }

    public Task PublishSupplierDeletedAsync(int supplierId, string name)
    {
        var evt = new SupplierDeletedEvent
        {
            Module = "Inventory",
            EventType = "SupplierDeleted",
            SupplierId = supplierId,
            Name = name
        };
        return PublishAsync("inventory.supplier.deleted", evt);
    }

    #endregion

    #region Sales Events

    public Task PublishSaleCreatedAsync(int saleId, string invoiceNumber, DateTime saleDate, string customerName, decimal subTotal, decimal taxAmount, decimal discountAmount, decimal totalAmount, string status, int itemsCount)
    {
        var evt = new SaleCreatedEvent
        {
            Module = "Sales",
            EventType = "SaleCreated",
            SaleId = saleId,
            InvoiceNumber = invoiceNumber,
            SaleDate = saleDate,
            CustomerName = customerName,
            SubTotal = subTotal,
            TaxAmount = taxAmount,
            DiscountAmount = discountAmount,
            TotalAmount = totalAmount,
            Status = status,
            ItemsCount = itemsCount
        };
        return PublishAsync("sales.sale.created", evt);
    }

    public Task PublishSaleUpdatedAsync(int saleId, string invoiceNumber, DateTime saleDate, string customerName, decimal subTotal, decimal taxAmount, decimal discountAmount, decimal totalAmount, string status)
    {
        var evt = new SaleUpdatedEvent
        {
            Module = "Sales",
            EventType = "SaleUpdated",
            SaleId = saleId,
            InvoiceNumber = invoiceNumber,
            SaleDate = saleDate,
            CustomerName = customerName,
            SubTotal = subTotal,
            TaxAmount = taxAmount,
            DiscountAmount = discountAmount,
            TotalAmount = totalAmount,
            Status = status
        };
        return PublishAsync("sales.sale.updated", evt);
    }

    public Task PublishSaleDeletedAsync(int saleId, string invoiceNumber)
    {
        var evt = new SaleDeletedEvent
        {
            Module = "Sales",
            EventType = "SaleDeleted",
            SaleId = saleId,
            InvoiceNumber = invoiceNumber
        };
        return PublishAsync("sales.sale.deleted", evt);
    }

    public Task PublishPaymentCreatedAsync(int paymentId, int saleId, decimal amount, string paymentMethod, string status, string referenceNumber, DateTime? paymentDate)
    {
        var evt = new PaymentCreatedEvent
        {
            Module = "Sales",
            EventType = "PaymentCreated",
            PaymentId = paymentId,
            SaleId = saleId,
            Amount = amount,
            PaymentMethod = paymentMethod,
            Status = status,
            ReferenceNumber = referenceNumber,
            PaymentDate = paymentDate
        };
        return PublishAsync("sales.payment.created", evt);
    }

    public Task PublishPaymentUpdatedAsync(int paymentId, int saleId, decimal amount, string paymentMethod, string status, string referenceNumber, DateTime? paymentDate)
    {
        var evt = new PaymentUpdatedEvent
        {
            Module = "Sales",
            EventType = "PaymentUpdated",
            PaymentId = paymentId,
            SaleId = saleId,
            Amount = amount,
            PaymentMethod = paymentMethod,
            Status = status,
            ReferenceNumber = referenceNumber,
            PaymentDate = paymentDate
        };
        return PublishAsync("sales.payment.updated", evt);
    }

    public Task PublishPaymentDeletedAsync(int paymentId, int saleId, string referenceNumber)
    {
        var evt = new PaymentDeletedEvent
        {
            Module = "Sales",
            EventType = "PaymentDeleted",
            PaymentId = paymentId,
            SaleId = saleId,
            ReferenceNumber = referenceNumber
        };
        return PublishAsync("sales.payment.deleted", evt);
    }

    #endregion
}
