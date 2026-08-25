# This App has built totally with AI Agent (kilo)
> **Used Technologies**
> - Used **MS Sql Server** as ***RDBSM***
> 
> - Used **.Net Core and Blazor** as ***Tech Stack*** (Server mode)
> 
> - Used **MudBlazor** as ***UI Component Library***
> 
> - Used **Entity Framework Core** as ***ORM***
> 
> - Used **Serilog** as ***Logging Framework***
> 
> - Used **Hangfire** as ***Background Job Processing***
>
> - Used **Redis** as ***Caching Provider***

## Project Structure

The solution follows a modular architecture with separate projects for each business domain:

| Module | Description |
|--------|-------------|
| **AAA** | Authentication & Authorization (Users, Roles, Permissions, Accounts, Transactions) |
| **HumanResource** | Employee management, Departments, Attendance, Leave management |
| **Inventory** | Products, Categories, Inbounds, Outbounds, Kardex, Suppliers, Customers |
| **Accounting** | Chart of Accounts, Journal Entries, Fiscal Years, Accounting Periods, Budgets, Trial Balances |
| **CRM** | Leads, Customers, Opportunities, Activities, Campaigns |
| **Sales** | Sales invoices, Sale items, Payments |
| **Hangfire** | Background jobs and scheduled tasks |

## Database Architecture

Each module has its own `DbContext`:

- `AAADbContext` - Users, roles, permissions, accounts, transactions
- `HumanResourceDbContext` - Employees, departments, attendance, leaves
- `InventoryDbContext` - Products, categories, inbounds, outbounds, kardex
- `AccountingDbContext` - Chart of accounts, journal entries, fiscal years, budgets
- `CRMDbContext` - Leads, opportunities, activities, campaigns
- `SaleDbContext` - Sales, sale items, payments

All contexts share the same SQL Server database (`ERPBlazorDb`).

## Key Features

### Authentication & Authorization
- Login/Logout functionality
- Role-based access control
- Permission management
- User session management

### Human Resource
- Employee management with department assignment
- Department hierarchy (parent/child departments)
- Attendance tracking
- Leave management with approval workflow

### Inventory Management
- Product catalog with categories
- Inbound/Outbound stock operations
- Kardex (inventory movement history)
- Supplier and customer management
- Stock level monitoring

### Accounting
- Chart of accounts with hierarchical structure
- Journal entries with debit/credit lines
- Fiscal year and accounting period management
- Budget planning and tracking
- Trial balance generation

### CRM
- Lead management with status tracking
- Customer relationship management
- Opportunity pipeline management
- Activity scheduling (calls, emails, meetings, tasks)
- Marketing campaign tracking

### Sales
- Sales invoice creation with multiple items
- Automatic total calculation (subtotal, tax, discount)
- Payment tracking with multiple payment methods
- Sale status management (Pending, Completed, Cancelled, Refunded)

### Background Jobs (Hangfire)
- Dashboard available at `/hangfire`
- Scheduled jobs:
  - Daily kardex cleanup (removes records older than 2 years)
  - Daily sales report generation
- Persistent job storage using SQL Server

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server
- Redis (for caching)

### Configuration
Update the connection strings in `appsettings.json`:
- `ERPBlazorDb` - Main database connection
- `redis` - Redis cache connection

### Database Migrations
Each module has its own migrations:

```bash
# HR Module
dotnet ef migrations add MigrationName --context HumanResourceDbContext --output-dir HumanResource/Data/Migrations

# AAA Module
dotnet ef migrations add MigrationName --context AAADbContext --output-dir AAA/Data/Migrations

# Accounting Module
dotnet ef migrations add MigrationName --context AccountingDbContext --output-dir Accounting/Data/Migrations

# CRM Module
dotnet ef migrations add MigrationName --context CRMDbContext --output-dir CRM/Data/Migrations

# Sales Module
dotnet ef migrations add MigrationName --context SaleDbContext --output-dir Sales/Data/Migrations
```

Apply migrations:
```bash
dotnet ef database update --context <DbContextName>
```

### Running the Application
```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5002`.

## Seeded Data

Default users for testing:
- `admin` / `admin123` (Administrator)
- `manager` / `manager123` (Manager)
- `employee` / `employee123` (Employee)

## Architecture Decisions

- **Multiple DbContexts**: Each business module has its own DbContext for better separation of concerns and maintainability.
- **Split Query Mode**: Configured on DbContexts with multiple collection includes to prevent Cartesian explosion and improve query performance.
- **Cascade Delete**: Used for dependent entities (e.g., SaleItems deleted when Sale is deleted).
- **Restrict Delete**: Used for referenced entities to prevent accidental data loss (e.g., Department employees).
- **Scoped Services**: All business logic services are registered as scoped for proper lifecycle management.

## Development Notes

- All queries with multiple collection includes use `.AsSplitQuery()` to avoid performance warnings.
- Hangfire dashboard is accessible at `/hangfire`.
- The application uses Serilog for structured logging.
- MudBlazor components are used throughout the UI for consistency.
