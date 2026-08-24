using ERPBlazorApp.HumanResource.Models;

namespace ERPBlazorApp.HumanResource.Data;

public static class HumanResourceSampleData
{
    public static List<Department> GetDepartments()
    {
        return new List<Department>
        {
            new Department { Id = 1, Name = "Corporate", Description = "Corporate Management", ManagerId = 2 },
            new Department { Id = 2, Name = "IT", Description = "Information Technology", ParentDepartmentId = 1, ManagerId = 1 },
            new Department { Id = 3, Name = "HR", Description = "Human Resources", ParentDepartmentId = 1, ManagerId = 2 },
            new Department { Id = 4, Name = "Finance", Description = "Finance and Accounting", ParentDepartmentId = 1, ManagerId = 3 },
            new Department { Id = 5, Name = "Sales", Description = "Sales and Marketing", ParentDepartmentId = 1, ManagerId = 4 },
            new Department { Id = 6, Name = "Development", Description = "Software Development", ParentDepartmentId = 2, ManagerId = 1 },
            new Department { Id = 7, Name = "QA", Description = "Quality Assurance", ParentDepartmentId = 2, ManagerId = 5 }
        };
    }

    public static List<Employee> GetEmployees()
    {
        return new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Ali", LastName = "Ahmadi", Email = "ali@erp.ir", Phone = "09121234567", Position = "Software Engineer", DepartmentId = 6, HireDate = new DateTime(2022, 1, 15), Salary = 25000000 },
            new Employee { Id = 2, FirstName = "Sara", LastName = "Mohammadi", Email = "sara@erp.ir", Phone = "09129876543", Position = "HR Manager", DepartmentId = 3, HireDate = new DateTime(2021, 3, 10), Salary = 28000000 },
            new Employee { Id = 3, FirstName = "Reza", LastName = "Hosseini", Email = "reza@erp.ir", Phone = "09135551234", Position = "Accountant", DepartmentId = 4, HireDate = new DateTime(2023, 6, 1), Salary = 22000000 },
            new Employee { Id = 4, FirstName = "Maryam", LastName = "Karimi", Email = "maryam@erp.ir", Phone = "09137778899", Position = "Sales Manager", DepartmentId = 5, HireDate = new DateTime(2020, 9, 20), Salary = 30000000 },
            new Employee { Id = 5, FirstName = "Hossein", LastName = "Rahimi", Email = "hossein@erp.ir", Phone = "09134445566", Position = "Developer", DepartmentId = 6, HireDate = new DateTime(2024, 1, 5), Salary = 20000000 }
        };
    }

    public static List<Attendance> GetAttendance()
    {
        var today = DateTime.Today;
        return new List<Attendance>
        {
            new Attendance { Id = 1, EmployeeId = 1, Date = today, CheckIn = new DateTime(today.Year, today.Month, today.Day, 8, 5, 0), CheckOut = new DateTime(today.Year, today.Month, today.Day, 17, 10, 0), Status = "Present" },
            new Attendance { Id = 2, EmployeeId = 2, Date = today, CheckIn = new DateTime(today.Year, today.Month, today.Day, 8, 30, 0), CheckOut = null, Status = "Present" },
            new Attendance { Id = 3, EmployeeId = 3, Date = today, CheckIn = null, CheckOut = null, Status = "Absent" }
        };
    }

    public static List<Leave> GetLeaves()
    {
        return new List<Leave>
        {
            new Leave { Id = 1, EmployeeId = 1, LeaveType = "Annual", StartDate = new DateTime(2026, 9, 1), EndDate = new DateTime(2026, 9, 5), Reason = "Vacation", Status = "Pending", RequestDate = new DateTime(2026, 8, 20) },
            new Leave { Id = 2, EmployeeId = 4, LeaveType = "Sick", StartDate = new DateTime(2026, 8, 15), EndDate = new DateTime(2026, 8, 16), Reason = "Flu", Status = "Approved", RequestDate = new DateTime(2026, 8, 14) }
        };
    }
}
